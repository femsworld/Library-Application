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
    search: string
    sort: string
  booksByGenre: Book[]
}

export interface FetchQuery {
    offset: number;
    limit: number;
  }

  export interface FetchSingleBookQuery {
    id: string | undefined;
  }

  export interface SearchBookQuery {
    search: string | undefined;
  }

  export interface SortBookQuery {
    sort: string | undefined;
  }

  export interface FetchQueryCategory {
    genre: string
    }

const initialState : BookReducer = {
    loading: false,
    error: "",
    books: [],
    genre: "",
    search: "",
    sort: "",
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
  );

  export const SearchBooksByTitle = createAsyncThunk(
    "SearchBooksByTitle",
    async ({ search}: SearchBookQuery) => {
        try {
            console.log("Reducer: ", search)
              const result = await axios.get<Book[]>(`${baseApi}/Books/search?searchTerm=${search}`)
              console.log("fetchBooksByGenre URL: ", result)
              return result.data
            // }
        } catch (e) {
            const error = e as AxiosError
            return error.message
        }
    }
  );
  
  export const SortBooks = createAsyncThunk(
    "SortBooks",
    async ({ sort}: SortBookQuery) => {
        try {
            console.log("Reducer: ", sort)
              const result = await axios.get<Book[]>(`${baseApi}/Books/sort?sortOrder==${sort}`)
              console.log("SortBooks URL: ", result)
              return result.data
            // }
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
              state.loading = false;
              state.error = action.payload;
            } else {
              const booksPayload = action.payload as { books: Book[] };
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
          .addCase(fetchSingleBook.pending, (state) => {
            state.loading = true;
          })
          .addCase(fetchSingleBook.rejected, (state) => {
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
        .addCase(SearchBooksByTitle.fulfilled, (state, action) => {
          if (typeof action.payload === "string") {
              state.error = action.payload
          } else {
            state.books = action.payload
          }
          state.loading = false
      })
      .addCase(SearchBooksByTitle.pending, (state) => {
          state.loading = true
      })
      .addCase(SearchBooksByTitle.rejected, (state) => {
          state.loading = false
          state.error = "This action cannot be completed at the moment, please try again later"
      })
      .addCase(SortBooks.pending, (state) => {
        state.loading = true;
      })
      .addCase(SortBooks.rejected, (state) => {
        state.loading = false;
        state.error =
          "This action cannot be completed at the moment, please try again later";
      })
      .addCase(SortBooks.fulfilled, (state, action) => {
        if (typeof action.payload === "string") {
          state.loading = false;
          state.error = action.payload;
        } else {
          state.books = action.payload;
        }
        state.loading = false;
      });
      },
})

const booksReducer = booksSlice.reducer
export const { cleanUpBookReducer, setGenre, selectGenre } = booksSlice.actions;
export default booksReducer