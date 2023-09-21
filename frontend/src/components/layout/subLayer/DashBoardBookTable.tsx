import { DataGrid, GridCellParams, GridColDef } from "@mui/x-data-grid";
import useAppSelector from "../../../hooks/useAppSelector";
import { useEffect, useState } from "react";
import useAppDispatch from "../../../hooks/useAppDispatch";
import {
  deleteBook,
  fetchAllBooks,
  fetchAllBooksQuery,
} from "../../../redux/reducers/booksReducer";
import { Button, Container } from "@mui/material";
import EditBook from "./EditBook";

export default function DashBoardBookTable() {
  const { books, loading, totalPages } = useAppSelector(
    (state) => state.booksReducer
  );
  const dispatch = useAppDispatch();
  const [paginationQuery, setPaginationQuery] = useState<fetchAllBooksQuery>({
    page: 0,
    pageSize: 5,
  });
  const [selectedBookId, setSelectedBookId] = useState<string | null>(null);

  const handleDeleteBook = async (bookId: string) => {
    if (bookId) {
      await dispatch(deleteBook(bookId));
      setSelectedBookId(null);
      console.log(`Deleting Book: ${bookId}`);
    }
  };

  const handleEditBook = (bookId: string) => {
    
    console.log(`Editing book with ID: ${bookId}`);
  };

  const buttonStyle = {
    width: "75px",
  };

  const columns: GridColDef[] = [
    { field: "title", headerName: "Title", width: 200 },
    { field: "genre", headerName: "Genre", width: 130 },
    {
      field: "actions",
      headerName: "Actions",
      width: 300,
      renderCell: (params: GridCellParams) => (
        <div style={{ display: "flex", alignItems: "center" }}>
          <EditBook id={params.row.id} />
          <span style={{ marginRight: "15px" }}></span>
          <Button
            variant="outlined"
            color="primary"
            onClick={() => handleDeleteBook(params.row.id as string)}
            style={buttonStyle}
          >
            Delete
          </Button>
        </div>
      ),
    },
  ];
  
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
