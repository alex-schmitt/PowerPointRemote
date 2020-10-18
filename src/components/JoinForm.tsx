import React, { useState } from 'react'
import { joinChannel } from '../channelApi'

const JoinForm: React.FC<{ setAccessToken: React.Dispatch<React.SetStateAction<string>>; channel: string }> = ({
  setAccessToken,
  channel,
}) => {
  const [userName, setUserName] = useState('')
  const [channelId, setChannelId] = useState(channel)

  const join = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    const response = await joinChannel(channelId, userName)
    setAccessToken(response.data.accessToken)
  }

  return (
    <form onSubmit={join}>
      <input type="text" value={userName} onChange={e => setUserName(e.target.value)} />
      <input type="text" value={channelId} onChange={e => setChannelId(e.target.value)} />
      <input type="submit" value="Join Remote" />
    </form>
  )
}

export default JoinForm
