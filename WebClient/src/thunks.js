import { createAsyncThunk } from "@reduxjs/toolkit";
import { channelApi } from "./api/channel";
import { serverMethod } from "./signalrMethod";

export const joinChannel = createAsyncThunk(
  "channel/joinChannel",
  async ({ userName, channelId }, { rejectWithValue }) => {
    try {
      const { data } = await channelApi.joinChannel(userName, channelId);
      return data;
    } catch (error) {
      return rejectWithValue(error.response.data);
    }
  }
);

export const refreshState = (type, signalrServerMethod) =>
  createAsyncThunk(type, async ({ hubConnection }, { rejectWithValue }) => {
    const { status, data } = await hubConnection.invoke(signalrServerMethod);
    if (status === 200) return data;
    else return rejectWithValue(data);
  });

export const refreshSlideShowState = refreshState(
  "slideShow/fetchState",
  serverMethod.getSlideShowState
);

export const refreshChannelState = refreshState("channel/fetchState", serverMethod.getChannelState);

export const sendSlideShowCommand = createAsyncThunk(
  "sender/sendSlideShowCommand",
  async ({ hubConnection, cmd }) => {
    const { status } = await hubConnection.invoke(serverMethod.sendSlideShowCommand);
    return status;
  }
);
