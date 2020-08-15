import { configureStore } from "@reduxjs/toolkit";
import { slice as slideShowSlice } from "./slideShow";
import { slice as connectionSlice } from "./connection";
import { slice as channelSlice } from "./channel";

export const store = configureStore({
  reducer: {
    slideShow: slideShowSlice.reducer,
    connection: connectionSlice.reducer,
    channel: channelSlice.reducer,
  },
});

export const { setConnectionStatus } = connectionSlice.actions;

export const { setSlideShowState, setSlidePosition, setSlideCount } = slideShowSlice.actions;

export const {
  setChannelState,
  setHostConnected,
  setHostDisconnected,
  setChannelEnded,
} = channelSlice.actions;
