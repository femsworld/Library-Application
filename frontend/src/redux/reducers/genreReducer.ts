import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Book } from "../../types/Book";
import axios, { AxiosError } from "axios";
import { baseApi } from "../common/baseApi";

interface GenreReducer {
  genre: string;
  booksByGenre: Book[]
  loading: boolean
  error: string
}

const initialState: GenreReducer = {
  genre: "",
  booksByGenre: [],
  loading: false,
  error: ""
};
   
  export interface FetchQueryCategory {
    genre: string
    }

  export const fetchBooksByGenre = createAsyncThunk(
    "fetchBooksByGenre",
    async ({ genre}: FetchQueryCategory) => {
        try {
            console.log("Reducer: ", genre)
            const result = await axios.get<Book[]>(`${baseApi}/Books/categorize?genre=${genre}`)
            return result.data
        } catch (e) {
            const error = e as AxiosError
            return error.message
        }
    }
  )
  
  const genreReducerSlice = createSlice({
    name: "genre",
    initialState,
    reducers: {
      setGenre: (state, action) => {
        state.genre = action.payload;
      },
      selectGenre: (state, action) => {
        state.genre = action.payload;
      },
    },
    extraReducers: (build) => {
      build
          .addCase(fetchBooksByGenre.fulfilled, (state, action) => {
              if (typeof action.payload === "string") {
                  state.error = action.payload
              } else {
                state.booksByGenre = action.payload
              }
              state.loading = false
          })
          .addCase(fetchBooksByGenre.pending, (state) => {
              state.loading = true
          })
          .addCase(fetchBooksByGenre.rejected, (state) => {
              state.loading = false
              state.error = "This action cannot be completed at the moment, please try again later"
          })
  }
  });
  
  const genreReducer = genreReducerSlice.reducer;
  
  export const { setGenre, selectGenre } = genreReducerSlice.actions;
  export default genreReducer;
  