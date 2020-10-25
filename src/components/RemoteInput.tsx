import React from 'react'
import styled from 'styled-components'
import { convertToPercentage } from '../utils'
import SvgBase from './SvgBase'

const Button = styled.button`
  position: relative;
  border-radius: 50%;
  outline: none;
  border: none;

  &:active {
    transform: translateY(0.1em);
  }
`

const remoteScale = 450

const buttonLeftScale = {
  top: 18,
  left: 2,
  width: 45,
  height: 45,
}

const buttonRightScale = {
  right: 16,
  borderWidth: 2.5,
  width: 65,
  height: 65,
}

const chevronLeftScale = {
  right: 2.5,
  width: 18,
}

const chevronRightScale = {
  left: 1.5,
  width: 26,
}

const scaleToPixels = (n: number): number => remoteScale * convertToPercentage(n)

const RemoteContainer = styled.div`
  white-space: nowrap;
  overflow: hidden;
  margin: 0 auto;

  @media all and (max-width: ${remoteScale}px) {
    width: ${buttonRightScale.width + buttonLeftScale.width - buttonRightScale.right}vw;
    height: ${buttonRightScale.height + buttonLeftScale.top}vw;
  }

  @media all and (min-width: ${remoteScale}px) {
    width: ${scaleToPixels(buttonRightScale.width) +
    scaleToPixels(buttonLeftScale.width) -
    scaleToPixels(buttonRightScale.right)}px;
    height: ${scaleToPixels(buttonRightScale.height) + scaleToPixels(buttonLeftScale.top)}px;
  }
`

const LeftButton = styled(Button)`
  background-color: #ebebeb;

  @media all and (max-width: ${remoteScale}px) {
    top: ${buttonLeftScale.top}vw;
    left: ${buttonLeftScale.left}vw;
    width: ${buttonLeftScale.width}vw;
    height: ${buttonLeftScale.height}vw;
  }

  @media all and (min-width: ${remoteScale}px) {
    top: ${scaleToPixels(buttonLeftScale.top)}px;
    left: ${scaleToPixels(buttonLeftScale.left)}px;
    width: ${scaleToPixels(buttonLeftScale.width)}px;
    height: ${scaleToPixels(buttonLeftScale.height)}px;
  }
`

const ButtonRight = styled(Button)`
  background-color: #d8d8d8;
  border: solid white;

  @media all and (max-width: ${remoteScale}px) {
    right: ${buttonRightScale.right}vw;
    border-width: ${buttonRightScale.borderWidth}vw;
    width: ${buttonRightScale.width}vw;
    height: ${buttonRightScale.height}vw;
  }

  @media all and (min-width: ${remoteScale}px) {
    right: ${scaleToPixels(buttonRightScale.right)}px;
    border-width: ${scaleToPixels(buttonRightScale.borderWidth)}px;
    width: ${scaleToPixels(buttonRightScale.width)}px;
    height: ${scaleToPixels(buttonRightScale.height)}px;
  }
`

const LeftChevronSvg = styled(SvgBase)`
  position: relative;

  @media all and (max-width: ${remoteScale}px) {
    right: ${chevronLeftScale.right}vw;
    width: ${chevronLeftScale.width}vw;
  }

  @media all and (min-width: ${remoteScale}px) {
    right: ${scaleToPixels(chevronLeftScale.right)}px;
    width: ${scaleToPixels(chevronLeftScale.width)}px;
  }
`

const RightChevronSvg = styled(SvgBase)`
  position: relative;

  @media all and (max-width: ${remoteScale}px) {
    left: ${chevronRightScale.left}vw;
    width: ${chevronRightScale.width}vw;
  }

  @media all and (min-width: ${remoteScale}px) {
    left: ${scaleToPixels(chevronRightScale.left)}px;
    width: ${scaleToPixels(chevronRightScale.width)}px;
  }
`

const RemoteInput: React.FC<{ onNextClick: () => void; onPreviousClick: () => void }> = ({
  onNextClick,
  onPreviousClick,
}) => {
  return (
    <RemoteContainer>
      <LeftButton onClick={onPreviousClick}>
        <LeftChevronSvg viewBox="0 0 320 512">
          <path
            fill="currentColor"
            d="M34.52 239.03L228.87 44.69c9.37-9.37 24.57-9.37 33.94 0l22.67 22.67c9.36 9.36 9.37 24.52.04 33.9L131.49 256l154.02 154.75c9.34 9.38 9.32 24.54-.04 33.9l-22.67 22.67c-9.37 9.37-24.57 9.37-33.94 0L34.52 272.97c-9.37-9.37-9.37-24.57 0-33.94z"
          />
        </LeftChevronSvg>
      </LeftButton>
      <ButtonRight onClick={onNextClick}>
        <RightChevronSvg viewBox="0 0 320 512">
          <path
            fill="currentColor"
            d="M285.476 272.971L91.132 467.314c-9.373 9.373-24.569 9.373-33.941 0l-22.667-22.667c-9.357-9.357-9.375-24.522-.04-33.901L188.505 256 34.484 101.255c-9.335-9.379-9.317-24.544.04-33.901l22.667-22.667c9.373-9.373 24.569-9.373 33.941 0L285.475 239.03c9.373 9.372 9.373 24.568.001 33.941z"
          />
        </RightChevronSvg>
      </ButtonRight>
    </RemoteContainer>
  )
}

export default RemoteInput
