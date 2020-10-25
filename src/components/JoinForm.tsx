import React, { useState } from 'react'
import { joinChannel, JoinChannelRequestData, JoinChannelResponseData } from '../channelApi'
import styled from 'styled-components'
import { useForm } from 'react-hook-form'
import { formValidation } from '../utils'

const Form = styled.form`
  box-sizing: border-box;
  max-width: 100%;
  width: 20em;
  margin: 0 auto;
`

const TextInput = styled.input.attrs({
  type: 'text',
})<{ error: boolean }>`
  padding: 0.6em;
  border-radius: 0.5em;
  outline: none;
  border: 1px solid ${props => (props.error ? 'red' : 'grey')};
  width: 100%;
  box-sizing: border-box;
`

const SubmitContainer = styled.div`
  text-align: right;
`

const SubmitInput = styled.input.attrs({
  type: 'submit',
})`
  padding: 0.6em 1em;
  border-radius: 0.5em;
  border: grey solid 1px;
  display: inline-block;
  margin-top: 0.8em;
`
const FormLabel = styled.label`
  display: inline-block;
  margin-top: 0.8em;
`

const InputError = styled.div`
  color: red;
`

const JoinForm: React.FC<{
  setAccessToken: React.Dispatch<React.SetStateAction<string>>
  channel: string
}> = ({ setAccessToken, channel }) => {
  const { register, handleSubmit, errors, setError } = useForm<JoinChannelRequestData>()

  const submitForm = async (data: JoinChannelRequestData) => {
    try {
      const response = await joinChannel(data)
      if (response.accessToken) setAccessToken(response.accessToken)
      if (response.errors) formValidation.addServerErrors(response.errors, setError)
      if (!response.accessToken && !response.errors) {
        // TODO
      }
    } catch (error) {
      // TODO
    }
  }

  return (
    <Form onSubmit={handleSubmit(submitForm)}>
      <FormLabel htmlFor="userName">Username:</FormLabel>
      <TextInput
        id="userName"
        name="userName"
        ref={register({
          required: { value: true, message: 'A username is required to connect' },
          minLength: { value: 2, message: 'Your username must be at least 2 characters' },
          maxLength: { value: 20, message: 'Your username must be less than or equal to 20 characters' },
        })}
        error={!!errors.userName}
      />
      {errors.userName && <InputError>{errors.userName.message}</InputError>}
      <FormLabel htmlFor="channelId">Remote ID:</FormLabel>
      <TextInput
        id="channelId"
        name="channelId"
        ref={register({
          required: { value: true, message: 'A remote ID is required to connect' },
        })}
        error={!!errors.channelId}
        defaultValue={channel}
      />
      {errors.channelId && <InputError>{errors.channelId.message}</InputError>}
      <SubmitContainer>
        <SubmitInput value="Join Remote" />
      </SubmitContainer>
    </Form>
  )
}

export default JoinForm
