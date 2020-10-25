import React from 'react'
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom'
import { createGlobalStyle } from 'styled-components'
import Home from './components/Home'
import MainHeader from './components/MainHeader'

const GlobalStyle = createGlobalStyle`
  body, input, label {
    font-family: Helvetica, Arial, sans-serif;
    font-size: 16px;
  }
`

const App = (): JSX.Element => {
  return (
    <>
      <GlobalStyle />
      <MainHeader />
      <Router>
        <Switch>
          <Route path="/:channelId" render={() => <Home />} />
          <Route path="/" render={() => <Home />} />
        </Switch>
      </Router>
    </>
  )
}

export default App
