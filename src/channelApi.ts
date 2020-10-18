import axios, { AxiosResponse } from 'axios'
import { API_ADDRESS } from './constants'

const api = axios.create({ baseURL: API_ADDRESS })

export const joinChannel = (channelId: string, userName: string): Promise<AxiosResponse<{ accessToken: string }>> =>
  api.post('/join-channel', { channelId, userName })
