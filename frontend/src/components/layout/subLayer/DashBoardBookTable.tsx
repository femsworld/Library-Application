// import { DataGrid, GridCellParams, GridColDef, GridValueGetterParams } from "@mui/x-data-grid";
// import useAppSelector from "../../../hooks/useAppSelector";
// import { useEffect, useState } from "react";
// import useAppDispatch from "../../../hooks/useAppDispatch";
// import {
//   deleteBook,
//   fetchAllBooks,
//   fetchAllBooksQuery,
// } from "../../../redux/reducers/booksReducer";
// import { Button, Container } from "@mui/material";
// import BookCard from "../BookCard";


// const columns: GridColDef[] = [
//   { field: "title", headerName: "Title", width: 200 },
//   { field: "genre", headerName: "Genre", width: 130 },
// ];

// export default function DashBoardBookTable() {
//   const { books, loading, totalPages } = useAppSelector(
//     (state) => state.booksReducer
//   );
//   const dispatch = useAppDispatch();
//   const [paginationQuery, setPaginationQuery] = useState<fetchAllBooksQuery>({
//     page: 0,
//     pageSize: 5,
//   });
//   const [selectedBookId, setSelectedBookId] = useState<string | null>(null);

//   const handleDeleteBook = async (bookId: string) => {
//     if (bookId) {
//       await dispatch(deleteBook(bookId));
//       setSelectedBookId(null);
//     }
//   };

//   useEffect(() => {
//     dispatch(fetchAllBooks(paginationQuery));
//   }, [paginationQuery]);

//   return (
//     <div style={{ height: 400, width: "100%" }}>
//       <Container maxWidth="md">
//         <DataGrid
//           rows={books}
//           columns={columns}
//           initialState={{
//             pagination: {
//               paginationModel: { page: 0, pageSize: 5 },
//             },
//           }}
//           pageSizeOptions={[5, 10]}
//           checkboxSelection
//         />
//         {/* <Button
//           variant="outlined"
//           color="secondary"
//           onClick={handleDeleteBook}
//           disabled={!selectedBookId}
//         >
//           Delete
//         </Button> */}
//       </Container>
//     </div>
//   );
// }


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
import BookCard from "../BookCard";

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
    }
  };

  const columns: GridColDef[] = [
    { field: "title", headerName: "Title", width: 200 },
    { field: "genre", headerName: "Genre", width: 130 },
    {
      field: "actions",
      headerName: "Actions",
      width: 200,
      renderCell: (params: GridCellParams) => (
        <div>
          <Button
            variant="outlined"
            // color="secondary"
            onClick={() => handleDeleteBook(params.row.id as string)}
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
