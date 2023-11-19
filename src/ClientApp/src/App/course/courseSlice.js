import {
  createAsyncThunk,
  createSelector,
  createSlice,
} from "@reduxjs/toolkit";
import axios from "../../jwt/axios";

export const getCourses = createAsyncThunk("courses/getCourses", async () => {
  try {
    const response = await axios.get(
      "http://localhost:5000/api/Course/GetAllCoursesByUserId"
    );
    return response.data.resource;
  } catch (error) {}
});

export const addCourse = createAsyncThunk("courses/addCourse", async(data)=>{
  var response = await axios.post("http://localhost:5000/api/course/AddCourse",data)
  return response?.data?.resource;
})
const courseList = (state) => state.course.courses;
export const selectCourses = createSelector([courseList], (cl) => cl);

const initialState = {
  courses: [],
  originalCourses:[]
};

const courseSlice = createSlice({
  name: "courses",
  initialState: initialState,
  reducers: {
    searchCourse: (state, { payload }) => {
      const filteredItems = state.originalCourses.filter(
        (course) =>
          course.title.toLowerCase().includes(payload.toLowerCase()) ||
          course.description.toLowerCase().includes(payload.toLowerCase())
      );
      state.courses=filteredItems
    },
  },
  extraReducers: {
    [getCourses.fulfilled]: (state, { payload }) => {
      state.courses = payload;
      state.originalCourses=payload;
    },
  },
});

export default courseSlice.reducer;
export const { searchCourse } = courseSlice.actions;
