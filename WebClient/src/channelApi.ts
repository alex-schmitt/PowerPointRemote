import { API_ADDRESS, JOIN_CHANNEL_ENDPOINT } from './constants'

export type JoinChannelRequestData = {
  userName: string
  channelId: string
}

export type JoinChannelResponseData = {
  accessToken?: string
  errors?: { [P in keyof JoinChannelRequestData]?: string[] }
}

export const joinChannel = async (data: JoinChannelRequestData): Promise<JoinChannelResponseData> => {
  const response = await fetch(API_ADDRESS + JOIN_CHANNEL_ENDPOINT, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  })
  return (await response.json()) as JoinChannelResponseData
}
