import { createSlice } from "@reduxjs/toolkit";


const userSlice = createSlice({
    name:"user",
    initialState:{
      nameSurname:''
    },
    reducers:{
        addUserInfos:(state,{payload})=>{
            state.nameSurname = payload;
        }
    }
})

export default userSlice.reducer;
export const {addUserInfos} = userSlice.actions;