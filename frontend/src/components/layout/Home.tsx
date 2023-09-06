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
import { useDebounce } from 'use-debounce';

const Home = () => {
  const dispatch = useAppDispatch();
  const { books, loading, totalPages } = useAppSelector((state) => state.booksReducer);
  const [pageNo, setPageNo] = useState(1);
  const [pageSize] = useState(3);
  const [genreState, setGenreState] = useState("All");
  const [sortingOrder, setSortingOrder] = useState("None")
  const [cartItemCount, setCartItemCount] = useState(0);
  const [searchString, setSearchString] = useState('');
  const [debouncedSearchTerm] = useDebounce(searchString, 300)

  const handleChange = (event: React.ChangeEvent<unknown>, value: number) => {
  setPageNo(value);
  dispatch(fetchAllBooks({
      page: value,
      pageSize: pageSize,
      genre: genreState,
      sort: sortingOrder,
      search: debouncedSearchTerm
    }
  ));
};

useEffect(() => {
  dispatch(fetchAllBooks({
      page: pageNo,
      pageSize: pageSize,
      genre: genreState,
      sort: sortingOrder,
      search: debouncedSearchTerm
    }
  ));
}, [sortingOrder, genreState, debouncedSearchTerm]);

const getGenreProps = (genre: string) => {
  setGenreState(genre);
  setPageNo(1)
};

const handleSortChange = (newSortingOrder: string) => {
  setSortingOrder(newSortingOrder);
};

const handleSearch = (searchString: string) => {
  setSearchString(searchString);
};

return (
  <div>
        <Header handleSearch={handleSearch} />
        <div style={{ marginTop: '4rem' }}>
        <SelectGenre getGenre={getGenreProps}/>
        </div>
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