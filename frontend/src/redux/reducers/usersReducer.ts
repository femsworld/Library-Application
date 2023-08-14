import { PayloadAction, createAsyncThunk, createSlice, isAction } from "@reduxjs/toolkit";
import axios, { Axios, AxiosError } from "axios";
import { User } from "../../types/User";
import { baseApi } from "../common/baseApi";
import { NewUser } from "../../types/NewUser";

export const fetchAllUsers = createAsyncThunk(
    "fetchAllUsers",
    async () => {
        try {
            const result = await axios.get<User[]>(`${baseApi}/users`)
            return result.data
        } catch (e) {
            const error = e as AxiosError
            if(error.request) {
                // return error.request
                console.log("error in request: ", error.request)
            } else {
                // return error.response?.data
                console.log("error resonse: ", error.response?.data)
            }
        }
    }
)

export const createOneUser = createAsyncThunk(
    "createOneUser", 
    async({email, password, name, avatar}: NewUser) => {
    try {
      const result = await axios.post<NewUser>(`${baseApi}/users`, { email: email, password: password, name: name, avatar: avatar })
      return result.data
    } catch (e) {
      const error = e as AxiosError
      return error
    }
    }
  )

interface UserReducer {
    users: User[];
    currentUser?: User;
    newUser: NewUser;
    loading: boolean;
    error: string;
  }

const initialState: UserReducer = {
    users: [],
    loading: false,
    error: "",
    newUser: {
      name: "",
      email: "",
      password: "",
      avatar: "",
    },
    currentUser: {
      name: "",
      email: "",
      avatar: "",
      role: "client",
      age: -1
    },
  };
const usersSlice = createSlice({
    name: "users",
    initialState,
    reducers: {
        createUser: (state, action: PayloadAction<User>) => {
            state.users.push(action.payload)
        },
        // updataUserReducer: (state, action: PayloadAction<User[]>) =>{
        //     return action.payload
        // }
    },
    extraReducers: (build) => {
        build
        //   .addCase(fetchAllUsers.fulfilled, (state, action) => {
        //     if (action.payload instanceof AxiosError) {
        //       state.error = action.payload.message;
        //     } else {
        //       state.users = action.payload;
        //     }
        //   })
        //   .addCase(fetchAllUsers.pending, (state, action) => {
        //     state.loading = true;
        //   })
        //   .addCase(fetchAllUsers.rejected, (state, action) => {
        //     state.error = "Cannot fetch data";
        //   })
        //   .addCase(EditMeUser.fulfilled, (state, action) => {
        //     if (action.payload instanceof AxiosError) {
        //       state.error = action.payload.message;
        //     } else {
        //       state.currentUser = action.payload;
        //     }
        //   })
        //   .addCase(EditMeUser.pending, (state) => {
        //     state.loading = true;
        //   })
        //   .addCase(EditMeUser.rejected, (state) => {
        //     state.error = "User cannot be update at the moment, try again later.";
        //   })
        //   .addCase(createOneUser.fulfilled, (state, action) => {
        //     if (action.payload instanceof AxiosError) {
        //       state.error = action.payload.message
        //     } else {
        //       state.newUser = action.payload
        //     }
        //   })
        //   .addCase(createOneUser.pending, (state) => {
        //     state.loading = true;
        //   })
        //   .addCase(createOneUser.rejected, (state) => {
        //     state.error = "User cannot be update at the moment, try again later.";
        //   })
      },
})

const userReducer = usersSlice.reducer

export const {createUser } = usersSlice.actions

export default userReducer