import React, { useState } from 'react';
import { FormControl, InputLabel, MenuItem, Select, SelectChangeEvent } from '@mui/material';
import useAppDispatch from '../../hooks/useAppDispatch';
import { SortBooks, fetchAllBooks, fetchAllBooksQuery } from '../../redux/reducers/booksReducer';


export interface SortBooksInAscOrDescProps {
  handleSortChange: (newSortAsc: string) => void;
}

    const SortBooksInAscOrDesc: React.FC<SortBooksInAscOrDescProps> = ({ handleSortChange }) => {
    const [sortOrder, setSortOrder] = useState('None');
    const dispatch = useAppDispatch();
    const [ paginationQuery, setPaginationQuery] = useState<fetchAllBooksQuery>({
      page: 1, pageSize: 6, sort: "",
    })

      const handleSortOrderChange = (event: SelectChangeEvent<string>) => {
        const newSortOrder = event.target.value as string;
        setSortOrder(newSortOrder);
        handleSortChange(newSortOrder);
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