import { DataGrid, GridColDef, GridValueGetterParams } from "@mui/x-data-grid";
import useAppSelector from "../../../hooks/useAppSelector";
import { useEffect, useState } from "react";
import useAppDispatch from "../../../hooks/useAppDispatch";
import {
  fetchAllBooks,
  fetchAllBooksQuery,
} from "../../../redux/reducers/booksReducer";
import { Container } from "@mui/material";

const columns: GridColDef[] = [
  { field: "title", headerName: "Title", width: 200 },
  { field: "genre", headerName: "Genre", width: 130 },
];

export default function DashBoardBookTable() {
  const { books, loading, totalPages } = useAppSelector(
    (state) => state.booksReducer
  );
  const dispatch = useAppDispatch();
  const [paginationQuery, setPaginationQuery] = useState<fetchAllBooksQuery>({
    page: 0,
    pageSize: 5,
  });

  useEffect(() => {
    dispatch(fetchAllBooks(paginationQuery));
  }, [paginationQuery]);

  return (
    <div style={{ height: 400, width: "100%" }}>
      <Container maxWidth="md">
        <DataGrid
          rows={books}
          columns={columns}
          initialState={{
            pagination: {
              paginationModel: { page: 0, pageSize: 5 },
            },
          }}
          pageSizeOptions={[5, 10]}
          checkboxSelection
        />
      </Container>
    </div>
  );
}
