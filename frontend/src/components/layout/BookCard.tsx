import { useDispatch } from "react-redux"
import { Book } from "../../types/Book"
import useAppSelector from "../../hooks/useAppSelector";
import { addItemToCart } from "../../redux/reducers/cartReducer";
import { IconButton } from "@mui/material";
import AddShoppingCartIcon from '@mui/icons-material/AddShoppingCart';
import { Link } from "react-router-dom";

interface BookCardProps{
    book: Book
}

const BookCard: React.FC<BookCardProps> = ({book}) => {
    const dispatch = useDispatch();
    const { items } = useAppSelector((state) => state.cartReducer);
    // const {  } = useAppSelector((state) => state.cartReducer);

    const addOneItemToCart = (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        event.preventDefault();
        dispatch(addItemToCart(book));
      };

  return (
    <div className="book-card">
      <h3>{book.title}</h3>
      <img
        src={
            book.images && book.images.length > 0
              ? book.images[0]
              : "https://islandpress.org/sites/default/files/default_book_cover_2015.jpg"
          }
        className="card-img-top"
        alt="Book Cover"
        style={{ width: "20%", height: "auto", borderRadius: "8px" }}
      />
      <IconButton onClick={(e) => addOneItemToCart(e)} size="large" aria-label="shopping cart" color="inherit">
        <AddShoppingCartIcon />
      </IconButton> 
      <Link to={`/bookDetails/${book.id}`}>
        <button>Detail</button>
      </Link>
    </div>
  )
}

export default BookCard