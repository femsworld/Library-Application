import * as React from 'react';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormHelperText from '@mui/material/FormHelperText';
import FormControl from '@mui/material/FormControl';
import Select, { SelectChangeEvent } from '@mui/material/Select';

interface GetGenreProps {
    getGenre(genre: string): void;
  }

const SelectGenre: React.FC<GetGenreProps> = ({getGenre}) => {
  const [genre, setGenre] = React.useState('All');

  const selectLabels = (event: SelectChangeEvent) => {
    setGenre(event.target.value);
    getGenre(event.target.value)
  };

  return (
    <div>
      <FormControl sx={{ m: 1, minWidth: 120 }}>
        <Select
          value={genre}
          onChange={selectLabels}
          displayEmpty
          inputProps={{ 'aria-label': 'Without label' }}
        >
          <MenuItem value="">
            {/* <em>None</em> */}
          </MenuItem>
          <MenuItem value={"All"}>All</MenuItem>
          <MenuItem value={"TextBooks"}>TextBooks</MenuItem>
          <MenuItem value={"Novel"}>Novel</MenuItem>
          <MenuItem value={"Fiction"}>Fiction</MenuItem>
          <MenuItem value={"ResearchPaper"}>ResearchPaper</MenuItem>
        </Select>
        {/* <FormHelperText>Without label</FormHelperText> */}
      </FormControl>
    </div>
  );
}

export default SelectGenre