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
  // const inCart = localStorage.getItem("cartItems");
  // const cartItems = inCart && JSON.parse(inCart)
  


  const handleClearCart = () => {
    const confirmed = window.confirm('Are you sure you want to empty your cart?');
    if (confirmed) {
    //   dispatch(clearCart());
    localStorage.removeItem('cartItems');
    setCartItems([])
    }
  };

  useEffect(() => {
    // const cartItem = inCart ? JSON.parse(inCart) : []
    // console.log('cartItemsFromStore ==', cartItemsFromStore)
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
  
    await dispatch(placeLoan({ loanBooks }));
    console.log("Dispatch books:", loanBooks)
  
    localStorage.removeItem('cartItems');
    setCartItems([]);
  };
  
  return (
    <div>
      <div>
        <Header />
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
              <button onClick={() => handleIncreaseQuantity(item.id)}>+</button>
              <button onClick={() => handleDecreaseQuantity(item.id)}>-</button>
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
