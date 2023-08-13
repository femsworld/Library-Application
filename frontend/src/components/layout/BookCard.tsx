import { useDispatch } from "react-redux"
import { Book } from "../../types/Book"
import useAppSelector from "../../hooks/useAppSelector";
import { addItemToCart } from "../../redux/reducers/cartReducer";
import { IconButton } from "@mui/material";
import AddShoppingCartIcon from '@mui/icons-material/AddShoppingCart';

interface BookCardProps{
    book: Book
}

const BookCard: React.FC<BookCardProps> = ({book}) => {
    const dispatch = useDispatch();
    const { items } = useAppSelector((state) => state.cartReducer);

    const addOneItemToCart = (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        event.preventDefault();
        dispatch(addItemToCart(book));
      };

  return (
    <div className="book-card">
      <h3>{book.title}</h3>
      <img
        src={
            book.images ? book.images[0] : "https://media.cnn.com/api/v1/images/stellar/prod/230124153647-01-monterey-park-vigil.jpg?c=16x9&q=w_800,c_fill"
          }
        className="card-img-top"
        alt="Book Cover"
        style={{ width: "100%", height: "auto", borderRadius: "8px" }}
      />
      <IconButton onClick={(e) => addOneItemToCart(e)} size="large" aria-label="shopping cart" color="inherit">
        <AddShoppingCartIcon />
      </IconButton> 
    </div>
  )
}

export default BookCard