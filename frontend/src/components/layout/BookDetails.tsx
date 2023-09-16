import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom';
import useAppDispatch from '../../hooks/useAppDispatch';
import useAppSelector from '../../hooks/useAppSelector';
import { fetchSingleBook } from '../../redux/reducers/booksReducer';
import Header from './Header';

const BookDetails = () => {
    const { id } = useParams();
    const dispatch = useAppDispatch();
    const { singleBook } = useAppSelector((state) => state.booksReducer);
    const [searchString, setSearchString] = useState('');

    useEffect(() => {
        dispatch(fetchSingleBook({ id }));
        
      }, [id]);


      const handleSearch = (searchString: string) => {
        setSearchString(searchString);
      };

  return (
    <div>
      <Header handleSearch={handleSearch}/>
      BookDetails
        <h4>Book Title {singleBook?.title} </h4>
        <h4> Category: {singleBook?.genre} </h4>
    </div>
  )
}

export default BookDetails