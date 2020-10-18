import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import JoinForm from './JoinForm'
import Remote from './Remote'
import { tokenRepository } from '../accessTokenRepository'

const Home: React.FC = () => {
  const { channelId } = useParams<{ channelId: string }>()
  const [accessToken, setAccessToken] = useState(
    channelId ? tokenRepository.getAccessToken(channelId)?.value || '' : ''
  )

  useEffect(() => {
    if (channelId && accessToken) {
      tokenRepository.upsertAccessToken(channelId, accessToken)
    }
  }, [channelId, accessToken])

  if (accessToken) return <Remote accessToken={accessToken} />

  return <JoinForm setAccessToken={setAccessToken} channel={channelId} />
}

export default Home
