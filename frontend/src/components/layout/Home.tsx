import { useEffect, useState } from 'react';
import useAppDispatch from '../../hooks/useAppDispatch';
import useAppSelector from '../../hooks/useAppSelector';
import { FetchQuery, fetchAllBooks } from '../../redux/reducers/booksReducer';
import BookCard from './BookCard';
import Header from './Header'
import { Pagination } from '@mui/material';
import { fetchBooksByGenre } from '../../redux/reducers/genreReducer';
import SelectGenre from './SelectGenre';

const Home = () => {
  const dispatch = useAppDispatch();
  const { books, loading } = useAppSelector((state) => state.booksReducer);
  const { booksByGenre } = useAppSelector((state) => state.genreReducer);
  const [page, setPage] = useState(1);
  const [genreState, setGenreState] = useState("");
  const [dataLoaded, setDataLoaded] = useState(false);

const handleChange = (event: React.ChangeEvent<unknown>, value: number) => {
  setPage(value);
  const fetchQuery: FetchQuery = {
    offset: value,
    limit: 6,
  };
  dispatch(fetchAllBooks(fetchQuery));
};

useEffect(() => {
  dispatch(fetchAllBooks({ offset: 1, limit: 6 }))
  // .then(() => {
  //   setDataLoaded(true);
  // })
  dispatch(fetchBooksByGenre({genre: genreState}))
  console.log("Genre: ", genreState)
  
}, [genreState])

const getGenreProps = ((genre: string) =>
{
  setGenreState(genre)
})

return (
  <div>
    <>
      {console.log("Genre: ", booksByGenre)}
    </>
        {/* <Header /> */}
        <SelectGenre getGenre={getGenreProps}/>
        <div className="book-grid">
      {/* {loading || !dataLoaded ? ( */}
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