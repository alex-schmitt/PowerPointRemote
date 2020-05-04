import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Home from "./components/Home";
import Channel from "./components/Channel";

const App = () => {
  return (
    <Router>
      <Switch>
        <Route path="/:channelId" component={Channel} />
        <Route path="/" component={Home} />
      </Switch>
    </Router>
  );
};

export default App;
