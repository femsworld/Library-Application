import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Book } from "../../types/Book";
import axios, { AxiosError } from "axios";

interface BookReducer {
    loading: boolean;
    error: string;
    books: Book[];
}

export interface FetchQuery {
    offset: number;
    limit: number;
  }

const initialState : BookReducer = {
    loading: false,
    error: "",
    books: [],
  };

const base_Api = "http://localhost:5292/api/v1";


export const fetchAllBooks = createAsyncThunk(
    "fetchAllBooks",
    async ({ offset, limit }: FetchQuery) => {
      try {
        const result = await axios.get<{books: Book[]}>(
          `${base_Api}/books?offset=${offset}&limit=${limit}`
        );
        return result.data;
      } catch (e) {
        const error = e as AxiosError;
        return error.message;
      }
    }
  );
const booksSlice = createSlice({
    name: "books",
    initialState,
    reducers: {
        cleanUpBookReducer: (state) => {
            return initialState;
        },
    },
    extraReducers: (build) => {
        build
          .addCase(fetchAllBooks.pending, (state) => {
            state.loading = true;
          })
          .addCase(fetchAllBooks.rejected, (state) => {
            state.loading = false;
            state.error =
              "This action cannot be completed at the moment, please try again later";
          })
          .addCase(fetchAllBooks.fulfilled, (state, action) => {
            if (typeof action.payload === "string") {
              console.log('Action Payload:', action.payload);
              state.loading = false;
              state.error = action.payload;
            } else {
              const booksPayload = action.payload as { books: Book[] };
              console.log('Action Payload:', booksPayload);
              state.loading = false;
              state.error = "";
              state.books = booksPayload.books;
            }
          })
      },
})

const booksReducer = booksSlice.reducer
export const { cleanUpBookReducer } = booksSlice.actions;
export default booksReducer