import { useEffect, useState } from 'react';
import useAppDispatch from '../../hooks/useAppDispatch';
import useAppSelector from '../../hooks/useAppSelector';
import { FetchQuery, SortBooks, fetchAllBooks, fetchAllBooksQuery, fetchBooksByGenre } from '../../redux/reducers/booksReducer';
import BookCard from './BookCard';
import Header from './Header'
import { Pagination } from '@mui/material';
import SelectGenre from './SelectGenre';
import { Book } from '../../types/Book';
import SortBooksInAscOrDesc from './SortBooksInAscOrDesc';

const Home = () => {
  const dispatch = useAppDispatch();
  const { books, loading, totalPages } = useAppSelector((state) => state.booksReducer);
  const [pageNo, setPageNo] = useState(1);
  const [genreState, setGenreState] = useState("");
  const [sortAsc, setSortAsc] = useState("None")
  const [cartItemCount, setCartItemCount] = useState(0);
  const [ paginationQuery, setPaginationQuery] = useState<fetchAllBooksQuery>({
    page: 1, pageSize: 6, genre: genreState,
    sort: sortAsc === 'Ascending' ? 'asc' : sortAsc === 'Descending' ? 'desc' : '',
  })

const handleChange = (event: React.ChangeEvent<unknown>, value: number) => {
  setPageNo(value);
  // const fetchQuery: fetchAllBooksQuery = {
  //   page: value,
  //   pageSize: 6,
  // };
  // setPaginationQuery(fetchQuery)
};



// useEffect(() => {
//   if (genreState === 'All') {
//     if (sortAsc === 'Ascending') {
//       dispatch(SortBooks({ sort: 'asc' }));
//     } else if (sortAsc === 'Descending') {
//       dispatch(SortBooks({ sort: 'desc' }));
//     } else {
//       dispatch(fetchAllBooks(paginationQuery));
//       if(paginationQuery)
//       {
//       }
//     }
//   } else {
//     if (sortAsc === 'Ascending') {
//       dispatch(fetchBooksByGenre({ genre: genreState }));
//       dispatch(SortBooks({ sort: 'asc' }));
//     } else if (sortAsc === 'Descending') {
//       dispatch(fetchBooksByGenre({ genre: genreState }));
//       dispatch(SortBooks({ sort: 'desc' }));
//     } else {
//       dispatch(fetchBooksByGenre({ genre: genreState }));
//     }
//   }
// }, [genreState, sortAsc, paginationQuery]);


// const getGenreProps = ((genre: string) =>
// {
//   setGenreState(genre)
// })

useEffect(() => {
  dispatch(fetchAllBooks(paginationQuery));
}, [paginationQuery]);



const getGenreProps = (genre: string) => {
  setGenreState(genre);

//   const fetchQuery: fetchAllBooksQuery = {
//     page: 1,
//     pageSize: 6,
//     genre,
//   };
//   setPaginationQuery(fetchQuery);
//   dispatch(fetchAllBooks(fetchQuery));
};

const handleSortChange = (newSortAsc: string) => {
  setSortAsc(newSortAsc);
};

return (
  <div>
        {/* <Header page={paginationQuery.page} pageSize={paginationQuery.pageSize}/> */}
        <Header page={pageNo} pageSize={paginationQuery.pageSize} />
        <div style={{ marginTop: '4rem' }}>
        <SelectGenre getGenre={getGenreProps}/>
        </div>
        {/* <SortBooksInAscOrDesc/> */}
        <SortBooksInAscOrDesc handleSortChange={handleSortChange} />
        <div className="book-grid">
      {loading ? (
          <p>Loading...</p>
        ) : (
          books.map((book) => (
            <div key={book.title}>
              <BookCard book={book} setCartItemCount={setCartItemCount} />
            </div>
          ))
        )}
      </div>
      <Pagination count={totalPages} page={pageNo} onChange={handleChange} />
  </div>
  )
}

export default Home