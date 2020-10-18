import React from 'react'
import styled from 'styled-components'

const Header = styled.h1`
  text-align: center;
  margin-bottom: 1.5em;
`

const MainHeader: React.FC = () => {
  return <Header>Power Point Remote</Header>
}

export default MainHeader
