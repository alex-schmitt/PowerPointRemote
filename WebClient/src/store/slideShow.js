import { createSlice } from "@reduxjs/toolkit";
import { refreshSlideShowState } from "../thunks";

const initialState = {
  started: false,
  slideCount: 0,
  slidePosition: 0,
};

export const slice = createSlice({
  name: "slideShow",
  initialState,
  reducers: {
    setSlideShowStarted: state => ({ ...state, started: true }),
    setSlideShowEnded: state => ({ ...state, started: false }),
    setSlideCount: (state, { payload }) => ({ ...state, slideCount: payload }),
    setSlidePosition: (state, { payload }) => ({ ...state, slidePosition: payload }),
  },
  extraReducers: {
    [refreshSlideShowState.fulfilled]: (state, { payload }) => ({ ...initialState, ...payload }),
  },
});
