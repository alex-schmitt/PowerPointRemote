import { useEffect, useMemo, useState } from 'react'
import { HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr'
import { API_ADDRESS } from '../constants'
import { clientMethod, serverMethod } from '../signalrMethod'

interface SignalRResponse<T> {
  status: number
  data: T
}

type ChannelService = {
  connectionState: HubConnectionState
  hasChannelEnded: boolean
  slidePosition: number
  slideCount: number
  nextSlide(): Promise<SignalRResponse<undefined>>
  previousSlide(): Promise<SignalRResponse<undefined>>
}

const useChannelService = (accessToken: string): ChannelService => {
  const [connectionState, setConnectionState] = useState<HubConnectionState>(HubConnectionState.Disconnected)
  const [hasChannelEnded, setHasChannelEnded] = useState(false)
  const [slidePosition, setSlidePosition] = useState(0)
  const [slideCount, setSlideCount] = useState(0)

  const hubConnection = useMemo(() => {
    return new HubConnectionBuilder()
      .withAutomaticReconnect()
      .withUrl(`${API_ADDRESS}/hub/user`, { accessTokenFactory: () => accessToken })
      .build()
  }, [accessToken])

  useEffect(() => {
    // Client methods
    const slideCountUpdated = (count: number) => setSlideCount(count)
    const slidePositionUpdate = (position: number) => setSlidePosition(position)
    const channelEnded = () => setHasChannelEnded(true)

    // Client method listeners
    hubConnection.on(clientMethod.SlideCountUpdated, slideCountUpdated)
    hubConnection.on(clientMethod.SlidePositionUpdated, slidePositionUpdate)
    hubConnection.on(clientMethod.ChannelEnded, channelEnded)

    hubConnection.onreconnected(() => setConnectionState(hubConnection.state))
    hubConnection.onreconnecting(() => setConnectionState(hubConnection.state))
    hubConnection.onclose(() => setConnectionState(hubConnection.state))

    // Effect
    ;(async () => {
      await hubConnection.start()

      const channelState = await hubConnection.invoke<
        SignalRResponse<{
          slideCount: number
          slidePosition: number
          channelEnded: boolean
        }>
      >(serverMethod.getChannelState)

      setConnectionState(hubConnection.state)
      setHasChannelEnded(channelState.data.channelEnded)
      setSlideCount(channelState.data.slideCount)
      setSlidePosition(channelState.data.slidePosition)
    })()

    // Effect cleanup
    return () => {
      hubConnection.off(clientMethod.SlideCountUpdated, slideCountUpdated)
      hubConnection.off(clientMethod.SlidePositionUpdated, slidePositionUpdate)
      hubConnection.off(clientMethod.ChannelEnded, channelEnded)
      hubConnection.stop() // Intentionally ignoring promise
    }
  }, [hubConnection])

  return {
    connectionState,
    hasChannelEnded,
    slidePosition,
    slideCount,
    nextSlide: () => hubConnection.invoke<SignalRResponse<undefined>>(serverMethod.sendSlideShowCommand, 0),
    previousSlide: () => hubConnection.invoke<SignalRResponse<undefined>>(serverMethod.sendSlideShowCommand, 1),
  }
}

export default useChannelService
