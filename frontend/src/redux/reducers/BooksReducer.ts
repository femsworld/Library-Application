import { createSlice } from "@reduxjs/toolkit";
import { Book } from "../../types/Book";

const initialState : Book[] = []
const booksSlice = createSlice({
    name: "books",
    initialState,
    reducers: {

    }
})

const booksReducer = booksSlice.reducer
export default booksReducer