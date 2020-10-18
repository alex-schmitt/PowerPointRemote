import React, { useState } from 'react'
import { joinChannel } from '../channelApi'
import styled from 'styled-components'

const Form = styled.form`
  box-sizing: border-box;
  max-width: 100%;
  width: 20em;
  margin: 0 auto;
`

const TextInput = styled.input.attrs({
  type: 'text',
})`
  padding: 0.6em;
  border-radius: 0.5em;
  margin-bottom: 0.8em;
  outline: none;
  border: grey solid 1px;
  width: 100%;
  box-sizing: border-box;
`

const SubmitContainer = styled.div`
  width: 100%;
  text-align: right;
`

const SubmitInput = styled.input.attrs({
  type: 'submit',
})`
  padding: 0.6em 1em;
  border-radius: 0.5em;
  border: grey solid 1px;
  display: inline-block;
`

const FormLabel = styled.label``

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
    <Form onSubmit={join}>
      <FormLabel htmlFor="userName">Username:</FormLabel>
      <TextInput id="userName" value={userName} onChange={e => setUserName(e.target.value)} />
      <FormLabel htmlFor="channelId">Remote ID:</FormLabel>
      <TextInput id="channelId" value={channelId} onChange={e => setChannelId(e.target.value)} />
      <SubmitContainer>
        <SubmitInput value="Join Remote" />
      </SubmitContainer>
    </Form>
  )
}

export default JoinForm
