import React, { useEffect, useState } from 'react';
import useAppDispatch from '../../hooks/useAppDispatch';
import useAppSelector from '../../hooks/useAppSelector';
import {
  addItemToCart,
  clearCart,
  removeItemFromCart,
  decreaseItemInCart,
  placeLoan,
} from '../../redux/reducers/cartReducer';
import Header from './Header';
import { CartItem } from '../../types/CartItem';

const CartPage = () => {
  const dispatch = useAppDispatch();
  const [cartItems, setCartItems] = useState<CartItem[]>([]);
  const cartItemsFromStore = useAppSelector((state) => state.cartReducer.items);
  const [searchString, setSearchString] = useState("");

  const handleClearCart = () => {
    const confirmed = window.confirm('Are you sure you want to empty your cart?');
    if (confirmed) {
    localStorage.removeItem('cartItems');
    setCartItems([])
    }
  };

  useEffect(() => {
    setCartItems(cartItemsFromStore)
  }, [cartItemsFromStore])

  const handleIncreaseQuantity = (id: string) => {
    dispatch(addItemToCart({ id }));
  };
  const handleDecreaseQuantity = (id: string) => {
    dispatch(decreaseItemInCart({ id }));
  };
  const handleDeleteQuantity = (id: string) => {
    dispatch(removeItemFromCart({ id }));
  };

  const LoanBooks = async () => {
    const loanBooks = cartItems.map((item) => ({
      bookId: item.id,
    }));
  
    const booksToLoan = cartItems.map((item) => item.title).join(', ');
  
    const confirmed = window.confirm(`Are you sure you want to loan these books? ${booksToLoan}`);
    
    if (confirmed) {
      await dispatch(placeLoan({ loanBooks }));
      localStorage.removeItem('cartItems');
      setCartItems([]);
    }
  };

  const handleSearch = (searchString: string) => {
    setSearchString(searchString);
  };
  
  return (
    <div>
      <div>
        <Header handleSearch={handleSearch}/>
      </div>
      <h2>Cart</h2>
      {cartItems?.length === 0 ? (
        <p>Your cart is empty.</p>
      ) : (
        <ul>
          {cartItems.map((item: CartItem) => (
            <li key={item.id}>
              <p>{item.title}</p>
              <p>Quantity: {item.quantity}</p>
              <button onClick={() => handleDeleteQuantity(item.id)}>Remove item</button>
            </li>
          ))}
        </ul>
      )}
      <button onClick={handleClearCart}>Clear Cart</button>
      <h2>
        <button onClick={LoanBooks}>Place Loan</button>
      </h2>
    </div>
  );
};

export default CartPage;
