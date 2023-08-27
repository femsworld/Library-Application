import React, { useState } from 'react';
import { FormControl, InputLabel, MenuItem, Select, SelectChangeEvent } from '@mui/material';
import useAppDispatch from '../../hooks/useAppDispatch';
import { SortBooks } from '../../redux/reducers/booksReducer';

// interface SortBooksProps {
//     getSort(sort: string): void;
//   }

//   const SortBooksInAscOrDesc: React.FC<SortBooksProps> = ({ getSort }) => {
  const SortBooksInAscOrDesc = () => {
    const [sortOrder, setSortOrder] = useState('Ascending');
    const dispatch = useAppDispatch();

    // const handleSortChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    //   const newSortOrder = event.target.value as string;
    //   setSortOrder(newSortOrder);
    //   getSort(newSortOrder);
    // //   dispatch(SortBooks({ sortOrder: newSortOrder }));
    // dispatch(SortBooks({ sort: sortOrder }));
    // };

    // const handleSortChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        const handleSortChange = (event: SelectChangeEvent<string>) => {
        const newSortOrder = event.target.value as string;
        setSortOrder(newSortOrder);
        // getSort(newSortOrder);
        dispatch(SortBooks({ sort: newSortOrder }));
        // if (sortAsc === "Ascending") {
        //     dispatch(SortBooks({ sort: sortAsc }));
        //   }
        //   else if (sortAsc === "Descending") {
        //     dispatch(SortBooks({ sort: sortAsc }));
        //   }
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