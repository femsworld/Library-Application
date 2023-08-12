import { configureStore } from "@reduxjs/toolkit";
import booksReducer from "./reducers/booksReducer";
import { type } from "os";
import userReducer from "./reducers/usersReducer";

const store = configureStore({
    reducer: {
        booksReducer,
        userReducer
    }
})

export type GlobalState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch

export default store