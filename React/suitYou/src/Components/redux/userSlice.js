import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import axios from 'axios'

export const login = createAsyncThunk('users/login',
    async ( arr, thunkAPI) => {
      const response = await  axios.get(`https://localhost:7247/api/User/${arr[0]}/${arr[1]}`);
      if(response.status===200)
        return response.data;
      alert("שם משתמש לא תקין");
      return;
    }
  )

const initialState = {
  currentUser:null,
}

export const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    incrementByAmount: (state, action) => {
      
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(login.fulfilled, (state, action) => {

       state.currentUser = action.payload;
       console.log(state.currentUser);
      })
  },
})

// Action creators are generated for each case reducer function
export const {  } = userSlice.actions

export default userSlice.reducer