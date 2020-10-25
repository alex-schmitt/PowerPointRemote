import React from 'react'
import useChannelService from '../hooks/useChannelService'
import RemoteInput from './RemoteInput'
import RemoteSlideCounter from './RemoteSlideCounter'

const Remote: React.FC<{ accessToken: string }> = ({ accessToken }) => {
  const { connectionState, slidePosition, slideCount, nextSlide, previousSlide } = useChannelService(accessToken)

  return (
    <>
      <RemoteInput onNextClick={nextSlide} onPreviousClick={previousSlide} />
      <RemoteSlideCounter slideCount={slideCount} slidePosition={slidePosition} connectionState={connectionState} />
    </>
  )
}

export default Remote
