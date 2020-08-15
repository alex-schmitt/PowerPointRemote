import { createSlice } from "@reduxjs/toolkit";
import { refreshChannelState } from "../thunks";

const initialState = {
  ended: false,
  hostConnected: false,
};

export const slice = createSlice({
  name: "channel",
  initialState,
  reducers: {
    setChannelState: state => state,
    setHostConnected: state => ({ ...state, hostConnected: true }),
    setHostDisconnected: state => ({ ...state, hostConnected: false }),
    setChannelEnded: state => ({ ...state, ended: true }),
  },
  extraReducers: {
    [refreshChannelState.fulfilled]: (state, { payload }) => ({ ...initialState, ...payload }),
  },
});
