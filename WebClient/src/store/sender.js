import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  sendingSlideShowCmd: false,
};

export const slice = createSlice({
  name: "sender",
  initialState,
});
