import { configureStore } from "@reduxjs/toolkit";
import courseSlice from "../App/course/courseSlice";

const store = configureStore({
  reducer: {
     course:courseSlice,
  },
});

export default store;