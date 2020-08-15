import { createSlice } from "@reduxjs/toolkit";
import { refreshSlideShowState } from "../thunks";

const initialState = {
  started: false,
  slideCount: 0,
  currentSlidePosition: 0,
};

export const slice = createSlice({
  name: "slideShow",
  initialState,
  reducers: {
    setSlideShowStarted: state => ({ ...state, started: true }),
    setSlideShowEnded: state => ({ ...state, started: false }),
    setSlideCount: (state, payload) => ({ ...state, slideCount: payload }),
    setCurrentSlideNumber: (state, payload) => ({ ...state, currentSlidePosition: payload }),
  },
  extraReducers: {
    [refreshSlideShowState.fulfilled]: (state, { payload }) => ({ ...initialState, ...payload }),
  },
});
