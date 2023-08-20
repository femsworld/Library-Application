import { useEffect, useState } from 'react';
import useAppDispatch from '../../hooks/useAppDispatch';
import useAppSelector from '../../hooks/useAppSelector';
import { FetchQuery, fetchAllBooks, fetchBooksByGenre } from '../../redux/reducers/booksReducer';
import BookCard from './BookCard';
import Header from './Header'
import { Pagination } from '@mui/material';
import SelectGenre from './SelectGenre';
import { Book } from '../../types/Book';

const Home = () => {
  const dispatch = useAppDispatch();
  const { books, loading } = useAppSelector((state) => state.booksReducer);
  const [page, setPage] = useState(1);
  const [genreState, setGenreState] = useState("All");

const handleChange = (event: React.ChangeEvent<unknown>, value: number) => {
  setPage(value);
  const fetchQuery: FetchQuery = {
    offset: value,
    limit: 6,
  };
  // dispatch(fetchAllBooks(fetchQuery));
};

useEffect(() => {
  if (genreState === 'All') {
    dispatch(fetchAllBooks());
  } else {
    dispatch(fetchBooksByGenre({ genre: genreState }));
  }
}, [genreState]);

const getGenreProps = ((genre: string) =>
{
  console.log("Get genre before set: ", genre)
  setGenreState(genre)
})

return (
  <div>
        <Header />
        <div style={{ marginTop: '4rem' }}>
        <SelectGenre getGenre={getGenreProps}/>
        </div>
        <div className="book-grid">
      {loading ? (
          <p>Loading...</p>
        ) : (
          books.map((book) => (
            <div key={book.title}>
              <BookCard book={book} />
            </div>
          ))
        )}
      </div>
      <Pagination count={100} page={page} onChange={handleChange} />
  </div>
  )
}

export default Home