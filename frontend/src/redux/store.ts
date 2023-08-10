import { configureStore } from "@reduxjs/toolkit";
import booksReducer from "./reducers/booksReducer";
import userReducer from "./reducers/usersReducer";
import { type } from "os";

const store = configureStore({
    reducer: {
        booksReducer,
        userReducer
    }
})

export type GlobalState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch

export default store