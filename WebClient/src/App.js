import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Home from "./components/Home";
import Channel from "./components/Channel";
import { Provider } from "react-redux";
import { store } from "./store";
import { createChannelService } from "./channelService";

const channelService = createChannelService(store);

const App = () => {
  return (
    <Provider store={store}>
      <Router>
        <Switch>
          <Route path="/:channelId" render={() => <Channel channelService={channelService} />} />
          <Route path="/" component={Home} />
        </Switch>
      </Router>
    </Provider>
  );
};

export default App;
