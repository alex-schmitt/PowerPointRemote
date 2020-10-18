import React, { useState } from 'react'
import { useParams } from 'react-router-dom'
import JoinForm from './JoinForm'
import Remote from './Remote'

const Home: React.FC = () => {
  const [accessToken, setAccessToken] = useState('')
  const { channelId } = useParams<{ channelId: string }>()

  if (accessToken) return <Remote accessToken={accessToken} />

  return <JoinForm setAccessToken={setAccessToken} channel={channelId} />
}

export default Home
