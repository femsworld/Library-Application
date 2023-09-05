import * as React from 'react';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormHelperText from '@mui/material/FormHelperText';
import FormControl from '@mui/material/FormControl';
import Select, { SelectChangeEvent } from '@mui/material/Select';
import { fetchAllBooks, fetchAllBooksQuery } from '../../redux/reducers/booksReducer';
import { useState } from 'react';
import useAppDispatch from '../../hooks/useAppDispatch';

interface GetGenreProps {
    getGenre(genre: string): void;
  }

const SelectGenre: React.FC<GetGenreProps> = ({getGenre}) => {
  const [genre, setGenre] = React.useState('');
  const [ paginationQuery, setPaginationQuery] = useState<fetchAllBooksQuery>({
    page: 1, pageSize: 6, genre: "",
  })
  
  const dispatch = useAppDispatch();

  const selectLabels = (event: SelectChangeEvent) => {
    const selectedGenre = event.target.value;
    setGenre(selectedGenre);
    // setGenre(event.target.value);
    // getGenre(event.target.value)
    setPaginationQuery((prevQuery) => ({
      ...prevQuery,
      genre: selectedGenre,
    }));
    dispatch(fetchAllBooks(paginationQuery));
    getGenre(selectedGenre);
  };

  return (
    <div>
      <FormControl sx={{ m: 1, minWidth: 120 }}>
      <InputLabel>Select Genre</InputLabel>
        <Select
          value={genre}
          onChange={selectLabels}
          displayEmpty
          inputProps={{ 'aria-label': 'Without label' }}
        >
          <MenuItem value="">
          </MenuItem>
          {/* <MenuItem value={"All"}>All</MenuItem> */}
          <MenuItem value={"TextBooks"}>TextBooks</MenuItem>
          <MenuItem value={"Novel"}>Novel</MenuItem>
          <MenuItem value={"Fiction"}>Fiction</MenuItem>
          <MenuItem value={"ResearchPaper"}>ResearchPaper</MenuItem>
        </Select>
      </FormControl>
    </div>
  );
}

export default SelectGenre