import React, { useEffect } from 'react'
import { useParams } from 'react-router-dom';
import useAppDispatch from '../../hooks/useAppDispatch';
import useAppSelector from '../../hooks/useAppSelector';
import { fetchSingleBook } from '../../redux/reducers/booksReducer';

const BookDetails = () => {
    const { id } = useParams();
    const dispatch = useAppDispatch();
    const { singleBook } = useAppSelector((state) => state.booksReducer);

    useEffect(() => {
        dispatch(fetchSingleBook({ id }));
        
      }, [id]);


  return (
    <div>BookDetails
        <h4> {singleBook?.title} </h4>
      <h4> Category: {singleBook?.genre} </h4>
    </div>
  )
}

export default BookDetails