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
    genre: string;
  booksByGenre: Book[]
}

export interface FetchQuery {
    offset: number;
    limit: number;
  }

  export interface FetchSingleBookQuery {
    id: string | undefined;
  }

  export interface FetchQueryCategory {
    genre: string
    }

const initialState : BookReducer = {
    loading: false,
    error: "",
    books: [],
    genre: "",
    booksByGenre: [],
    singleBook: {
      id: "",
      title: "",
      images: [""],
      genre: ""

    },
  };


export const fetchAllBooks = createAsyncThunk(
    "fetchAllBooks",
    async () => {
      try {
        console.log("fetchAllBooks!!!!!!!")
        const result = await axios.get<{books: Book[]}>(
          `${baseApi}/books`
        );
        return result.data;
      } catch (e) {
        const error = e as AxiosError;
        return error.message;
      }
    }
  );

  export const fetchBooksByGenre = createAsyncThunk(
    "fetchBooksByGenre",
    async ({ genre}: FetchQueryCategory) => {
        try {
            console.log("Reducer: ", genre)
            const result = await axios.get<Book[]>(`${baseApi}/Books/categorize?genre=${genre}`)
            console.log("fetchBooksByGenre URL: ", result)
            return result.data
        } catch (e) {
            const error = e as AxiosError
            return error.message
        }
    }
  ) 

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
        setGenre: (state, action) => {
          state.genre = action.payload;
        },
        selectGenre: (state, action) => {
          state.genre = action.payload;
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
          })
          .addCase(fetchBooksByGenre.fulfilled, (state, action) => {
            if (typeof action.payload === "string") {
                state.error = action.payload
            } else {
              state.books = action.payload
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
      },
})

const booksReducer = booksSlice.reducer
export const { cleanUpBookReducer, setGenre, selectGenre } = booksSlice.actions;
export default booksReducer