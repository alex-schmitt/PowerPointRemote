import { createSlice } from "@reduxjs/toolkit";
import { joinChannel } from "../thunks";

const initialState = {
  status: "Disconnected",
  userName: null,
  channelId: null,
  error: null,
  accessToken: null,
};

export const slice = createSlice({
  name: "channel",
  initialState,
  reducers: {
    setConnectionStatus: (state, { payload }) => ({ ...state, status: payload }),
  },
  extraReducers: {
    [joinChannel.pending]: (state, { meta }) => {
      const { userName, channelId } = meta.arg;
      return { ...initialState, userName, channelId };
    },
    [joinChannel.fulfilled]: (state, action) => {
      const { accessToken } = action.payload;
      return { ...state, accessToken };
    },
    [joinChannel.rejected]: (state, action) => {
      const { payload } = action;
      if (payload) {
        return { ...state, error: payload };
      }
    },
  },
});
