import { useEffect, useState } from 'react';
import useAppDispatch from '../../hooks/useAppDispatch';
import useAppSelector from '../../hooks/useAppSelector';
import { FetchQuery, fetchAllBooks } from '../../redux/reducers/booksReducer';
import { Book } from '../../types/Book';
import BookCard from './BookCard';
import Header from './Header'
import { Pagination } from '@mui/material';


// const getBookList = (books: Book[]) => {
//   return books
// };

const Home = () => {
  const dispatch = useAppDispatch();
  const { books, loading } = useAppSelector((state) => state.booksReducer);
  const [page, setPage] = useState(1);
  const [dataLoaded, setDataLoaded] = useState(false);

// const filterBooks = getBookList(books);

const handleChange = (event: React.ChangeEvent<unknown>, value: number) => {
  setPage(value);
  const fetchQuery: FetchQuery = {
    offset: value,
    limit: 6,
  };
  dispatch(fetchAllBooks(fetchQuery));
};

useEffect(() => {
  console.log('Fetching books...');
  dispatch(fetchAllBooks({ offset: 1, limit: 6 }))
  .then(() => {
    setDataLoaded(true);
  })
}, [])

console.log('Rendering books:', books);

  return (
    <div>
        <Header />
        <div>
        {/* {filterBooks.map((book) => (
          <div key={book.title}>
            <BookCard book={book} />
          </div>
        ))} */}
        {/* {books.map((book) => (
        <div key={book.title}>
        <BookCard book={book} />
        </div>
        ))} */}
        {/* {books.length > 0 ? (
        books.map((book) => (
      <div div key={book.title}>
      <BookCard book={book} />
    </div>
  ))
) : (
  <p>Loading...</p>
)} */}
      {loading || !dataLoaded ? ( // Conditionally render loading state
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