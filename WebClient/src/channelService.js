import { HubConnectionBuilder } from "@microsoft/signalr";
import { apiAddress } from "./constants";
import { refreshChannelState, refreshSlideShowState, sendSlideShowCommand } from "./thunks";
import { setConnectionStatus, setHostConnected, setHostDisconnected } from "./store";
import { clientMethod } from "./signalrMethod";

const createHubConnection = accessToken =>
  new HubConnectionBuilder()
    .withAutomaticReconnect()
    .withUrl(`${apiAddress}/hub/user`, { accessTokenFactory: () => accessToken })
    .build();

export const registerChannelListeners = (hubConnection, store) => {
  hubConnection.on(clientMethod.HostConnected, () => store.dispatch(setHostConnected(undefined)));
  hubConnection.on(clientMethod.HostDisconnected, () =>
    store.dispatch(setHostDisconnected(undefined))
  );
  hubConnection.onreconnected(() => store.dispatch(setConnectionStatus(hubConnection.state)));
  hubConnection.onreconnecting(() => store.dispatch(setConnectionStatus(hubConnection.state)));
  hubConnection.onclose(() => store.dispatch(setConnectionStatus(hubConnection.state)));
};

export const createChannelService = store => {
  let hubConnection = null;

  const start = async accessToken => {
    if (hubConnection !== null) {
      throw "close current connection first";
    }
    hubConnection = createHubConnection(accessToken);
    registerChannelListeners(hubConnection, store);
    await hubConnection.start();
    store.dispatch(setConnectionStatus(hubConnection.state));
    store.dispatch(refreshSlideShowState({ hubConnection }));
    store.dispatch(refreshChannelState({ hubConnection }));
  };

  return {
    start,
    sendSlideShowCommand: async cmd => sendSlideShowCommand({ hubConnection, cmd }),
  };
};
