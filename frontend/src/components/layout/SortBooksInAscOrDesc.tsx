import React, { useState } from 'react';
import { FormControl, InputLabel, MenuItem, Select, SelectChangeEvent } from '@mui/material';
import useAppDispatch from '../../hooks/useAppDispatch';
import { SortBooks } from '../../redux/reducers/booksReducer';

  const SortBooksInAscOrDesc = () => {
    const [sortOrder, setSortOrder] = useState('Ascending');
    const dispatch = useAppDispatch();

        const handleSortChange = (event: SelectChangeEvent<string>) => {
        const newSortOrder = event.target.value as string;
        setSortOrder(newSortOrder);
        dispatch(SortBooks({ sort: newSortOrder }));
      };
  
    return (
      <FormControl>
        <InputLabel>Sort Order</InputLabel>
        <Select value={sortOrder} onChange={handleSortChange}>
          <MenuItem value="None">None</MenuItem>
          <MenuItem value="Ascending">Ascending</MenuItem>
          <MenuItem value="Descending">Descending</MenuItem>
        </Select>
      </FormControl>
    );
  };
  
  export default SortBooksInAscOrDesc