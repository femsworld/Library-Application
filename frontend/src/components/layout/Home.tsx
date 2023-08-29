import { useEffect, useState } from 'react';
import useAppDispatch from '../../hooks/useAppDispatch';
import useAppSelector from '../../hooks/useAppSelector';
import { FetchQuery, SortBooks, fetchAllBooks, fetchBooksByGenre } from '../../redux/reducers/booksReducer';
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
  const [genreState, setGenreState] = useState("All");
  const [sortAsc, setSortAsc] = useState("None")
  const [ paginationQuery, setPaginationQuery] = useState<FetchQuery>({
    offset: 1,
    limit: 6,
  })
  const [cartItemCount, setCartItemCount] = useState(0);

const handleChange = (event: React.ChangeEvent<unknown>, value: number) => {
  setPageNo(value);
  const fetchQuery: FetchQuery = {
    offset: value,
    limit: 6,
  };
  setPaginationQuery(fetchQuery)
};

useEffect(() => {
  if (genreState === 'All') {
    console.log("SortOrder:", sortAsc)
    if (sortAsc === 'Ascending') {
      dispatch(SortBooks({ sort: 'asc' }));
    } else if (sortAsc === 'Descending') {
      dispatch(SortBooks({ sort: 'desc' }));
    } else {
      dispatch(fetchAllBooks(paginationQuery));
      if(paginationQuery)
      {
      }
    }
  } else {
    if (sortAsc === 'Ascending') {
      dispatch(fetchBooksByGenre({ genre: genreState }));
      dispatch(SortBooks({ sort: 'asc' }));
    } else if (sortAsc === 'Descending') {
      dispatch(fetchBooksByGenre({ genre: genreState }));
      dispatch(SortBooks({ sort: 'desc' }));
    } else {
      dispatch(fetchBooksByGenre({ genre: genreState }));
    }
  }
}, [genreState, sortAsc, paginationQuery]);

const getGenreProps = ((genre: string) =>
{
  console.log("Get genre before set: ", genre)
  setGenreState(genre)
})

return (
  <div>
        <Header offset={paginationQuery.offset} limit={paginationQuery.limit}/>
        <div style={{ marginTop: '4rem' }}>
        <SelectGenre getGenre={getGenreProps}/>
        </div>
        <SortBooksInAscOrDesc/>
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