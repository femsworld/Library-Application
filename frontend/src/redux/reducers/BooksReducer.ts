import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Book } from "../../types/Book";
import axios, { AxiosError } from "axios";
import { baseApi } from "../common/baseApi";
import { SingleBook } from "../../types/SingleBook";

interface BookReducer {
    loading: boolean;
    error: string;
    books: Book[];
    singleBook: SingleBook;
}

export interface FetchQuery {
    offset: number;
    limit: number;
  }

  export interface FetchSingleBookQuery {
    id: string | undefined;
  }

const initialState : BookReducer = {
    loading: false,
    error: "",
    books: [],
    singleBook: {
      id: "",
      title: "",
      images: [""],
      genre: ""

    },
  };


export const fetchAllBooks = createAsyncThunk(
    "fetchAllBooks",
    async ({ offset, limit }: FetchQuery) => {
      try {
        const result = await axios.get<{books: Book[]}>(
          `${baseApi}/books?offset=${offset}&limit=${limit}`
        );
        return result.data;
      } catch (e) {
        const error = e as AxiosError;
        return error.message;
      }
    }
  );

  export const fetchSingleBook = createAsyncThunk(
    "fetchSingleBook",
    async ({ id }: FetchSingleBookQuery) => {
      try {
        console.log("Book Id: ", id)
        const result = await axios.get<SingleBook>(
          `${baseApi}/books/${id}`
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
        cleanUpBookReducer: () => {
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
              // console.log('Action Payload:', booksPayload);
              state.loading = false;
              state.error = "";
              state.books = booksPayload.books;
            }
          })
          .addCase(fetchSingleBook.fulfilled, (state, action) => {
            if (typeof action.payload === "string") {
              state.error = action.payload;
            } else {
              state.singleBook = action.payload;
            }
            state.loading = false;
          })
          .addCase(fetchSingleBook.pending, (state, action) => {
            state.loading = true;
          })
          .addCase(fetchSingleBook.rejected, (state, action) => {
            state.loading = false;
            state.error =
              "This action cannot be completed at the moment, please try again later";
          });
      },
})

const booksReducer = booksSlice.reducer
export const { cleanUpBookReducer } = booksSlice.actions;
export default booksReducer