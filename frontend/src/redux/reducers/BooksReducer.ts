import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Book } from "../../types/Book";
import axios, { AxiosError } from "axios";
import { baseApi } from "../common/baseApi";
import { SingleBook } from "../../types/SingleBook";
import { NewBook } from "../../types/NewBook";

interface BookReducer {
    loading: boolean;
    error: string;
    books: Book[];
    newBook: NewBook
    singleBook: SingleBook;
    genre: string;
    search: string
    sort: string
    totalPages: number
    booksByGenre: Book[]
}

export interface FetchQuery {
    page: number;
    pageSize: number;
  }

  export interface FetchSingleBookQuery {
    id: string | undefined;
  }

  export interface SearchBookQuery {
    search: string | undefined;
  }

  export interface SortBookQuery {
    sort: string;
  }

  export interface FetchQueryCategory {
    genre: string
    }

  export interface BooksWithPagination {
    books: Book[];
    totalPages: number
  }

  export interface fetchAllBooksQuery {
    genre?: string;
    sort?: string;
    search?: string | undefined;
    page?: number;
    pageSize?: number;
  }

const initialState : BookReducer = {
    loading: false,
    error: "",
    books: [],
    genre: "",
    search: "",
    sort: "",
    totalPages: 1,
    booksByGenre: [],
    singleBook: {
      id: "",
      title: "",
      images: [""],
      genre: ""
    },
    newBook: {
      title: "",
      images: [""],
      genre: ""
    },
  };

  //'http://localhost:5292/api/v1/books?page=1&pageSize=6&genre=Novel&sortOrder=Ascending&search=Ake'
// export const fetchAllBooks = createAsyncThunk(
//     "fetchAllBooks", 
//     async ({ page, pageSize }: FetchQuery) => {
//       try {
//         if(page < 0 && pageSize < 0)
//         {
//           const result = await axios.get<{books: Book[]}>(
//             `${baseApi}/books`
//           );
//           return result.data;
//         }
//         const result = await axios.get<{books: Book[]}>(
//           `${baseApi}/books?page=${page}&pageSize=${pageSize}`
//         );
//         return result.data;
//       } catch (e) {
//         const error = e as AxiosError;
//         return error.message;
//       }
//     }
//   );

export const fetchAllBooks = createAsyncThunk(
  "fetchAllBooks", 
  async ({ page, pageSize, genre, search, sort }: fetchAllBooksQuery) => {
    try {
      let endpoint = `${baseApi}/books?page=${page}&pageSize=${pageSize}`
      if(genre){
        endpoint += `&genre=${genre}`
      } 
      if(search){
        endpoint += `&search=${search}`
      }
      if(sort){
        endpoint += `&sort=${sort}`
      }
      
      const result = await axios.get<{books: Book[]}>(endpoint);
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
            const result = await axios.get<Book[]>(`${baseApi}/Books/categorize?genre=${genre}`)
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
              const result = await axios.get<Book[]>(`${baseApi}/Books/search?searchTerm=${search}`)
              return result.data
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
              const result = await axios.get<Book[]>(`${baseApi}/Books/sort?sortOrder=${sort}`)
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

  export const createOneBook = createAsyncThunk(
    "createOneBook", 
    async({title, genre, images }: NewBook) => {
    try {
      const ProfileToken = localStorage.getItem('loginResponse')
      const resultToken = ProfileToken && JSON.parse(ProfileToken)
      const result = await axios.post<NewBook>(`${baseApi}/Books`, { title: title, genre: genre, images: images }, { headers: { Authorization: `Bearer ${resultToken}` } })
      return result.data
    } catch (e) {
      const error = e as AxiosError
      return error
    }
    }
  )

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
              const booksPayload = action.payload as BooksWithPagination;
              state.loading = false;
              state.error = "";
              state.books = booksPayload.books;
              state.totalPages = booksPayload.totalPages;
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
      })
      .addCase(createOneBook.fulfilled, (state, action) => {
        if (action.payload instanceof AxiosError) {
          state.error = action.payload.message
        } else {
          state.newBook = action.payload
        }
      })
      .addCase(createOneBook.pending, (state) => {
        state.loading = true;
      })
      .addCase(createOneBook.rejected, (state) => {
        state.error = "Your book cannot be created at the moment, please try again later.";
      });
    },
})

const booksReducer = booksSlice.reducer
export const { cleanUpBookReducer, setGenre, selectGenre } = booksSlice.actions;
export default booksReducer