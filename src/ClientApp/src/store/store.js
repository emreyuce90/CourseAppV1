import { configureStore } from "@reduxjs/toolkit";
import courseSlice from "../App/course/courseSlice";
import userSlice from "../App/auth/store";



const store = configureStore({
  reducer: {
    course: courseSlice,
    user:userSlice
  },
});

export default store;
