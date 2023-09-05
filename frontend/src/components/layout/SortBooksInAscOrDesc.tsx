import React, { useState } from 'react';
import { FormControl, InputLabel, MenuItem, Select, SelectChangeEvent } from '@mui/material';
import useAppDispatch from '../../hooks/useAppDispatch';
import { SortBooks, fetchAllBooks, fetchAllBooksQuery } from '../../redux/reducers/booksReducer';


export interface SortBooksInAscOrDescProps {
  handleSortChange: (newSortAsc: string) => void;
}


  // const SortBooksInAscOrDesc = () => {
    const SortBooksInAscOrDesc: React.FC<SortBooksInAscOrDescProps> = ({ handleSortChange }) => {
    const [sortOrder, setSortOrder] = useState('Ascending');
    const dispatch = useAppDispatch();
    const [ paginationQuery, setPaginationQuery] = useState<fetchAllBooksQuery>({
      page: 1, pageSize: 6, sort: "",
    })

      // const handleSortChange = (event: SelectChangeEvent<string>) => {
      //   const newSortOrder = event.target.value as string;
      //   setSortOrder(newSortOrder);
      //   dispatch(SortBooks({ sort: newSortOrder }));
      // };

      const handleSortOrderChange = (event: SelectChangeEvent<string>) => {
        const newSortOrder = event.target.value as string;
        setSortOrder(newSortOrder);
        handleSortChange(newSortOrder);
      
        // const fetchQuery: fetchAllBooksQuery = {
        //   page: 1, // Set the page number to 1
        //   pageSize: 6,
        //   sort: newSortOrder, // Include the selected sort order
        // };
        // setPaginationQuery(fetchQuery);
        // dispatch(fetchAllBooks(fetchQuery));
      };
      
  
    return (
      <FormControl>
        <InputLabel>Sort Order</InputLabel>
        <Select value={sortOrder} onChange={handleSortOrderChange}>
          <MenuItem value="None">None</MenuItem>
          <MenuItem value="Ascending">Ascending</MenuItem>
          <MenuItem value="Descending">Descending</MenuItem>
        </Select>
      </FormControl>
    );
  };
  
  export default SortBooksInAscOrDesc