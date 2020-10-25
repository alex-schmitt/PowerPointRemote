import React from 'react'
import styled from 'styled-components'
import { HubConnectionState } from '@microsoft/signalr'

const SlideCountContainer = styled.div`
  text-align: center;
  font-size: 1.2em;
`

const RemoteSlideCounter: React.FC<{
  slidePosition: number
  slideCount: number
  connectionState: HubConnectionState
}> = ({ slidePosition, slideCount, connectionState }) => {
  if (connectionState === HubConnectionState.Connected) {
    if (slideCount === 0) {
      return <SlideCountContainer>Waiting for presentation to begin</SlideCountContainer>
    }

    return (
      <SlideCountContainer>
        Slide {slidePosition} of {slideCount}
      </SlideCountContainer>
    )
  }

  return <SlideCountContainer>{connectionState}</SlideCountContainer>
}

export default RemoteSlideCounter
