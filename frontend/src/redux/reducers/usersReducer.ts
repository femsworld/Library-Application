import { PayloadAction, createAsyncThunk, createSlice, isAction } from "@reduxjs/toolkit";
import axios, { Axios, AxiosError } from "axios";
import { User } from "../../types/User";

export const fetchAllUsers = createAsyncThunk(
    "fetchAllUsers",
    async () => {
        try {
            const result = await axios.get<User[]>("http://localhost:5292/api/v1/users")
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

const initialState : User[] = []
const usersSlice = createSlice({
    name: "users",
    initialState,
    reducers: {
        createUser: (state, action: PayloadAction<User>) => {
            state.push(action.payload)
        },
        updataUserReducer: (state, action: PayloadAction<User[]>) =>{
            return action.payload
        }
    },
    extraReducers: (build) => {
        build.addCase(fetchAllUsers.fulfilled, (state, action) => {
            if(action.payload) {
                return action.payload
            }
        })
    }
})

const userReducer = usersSlice.reducer

export const {createUser, updataUserReducer } = usersSlice.actions

export default userReducer