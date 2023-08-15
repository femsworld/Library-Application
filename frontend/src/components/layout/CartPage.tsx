import React from 'react';
import useAppDispatch from '../../hooks/useAppDispatch';
import useAppSelector from '../../hooks/useAppSelector';
import {
  addItemToCart,
  clearCart,
  removeItemFromCart,
  removeItemToCart,
} from '../../redux/reducers/cartReducer';
import Header from './Header';
import { CartItem } from '../../types/CartItem';

const CartPage = () => {
  const dispatch = useAppDispatch();
//   const cartItems = useAppSelector((state) => state.cartReducer.items);
  const inCart = localStorage.getItem("cartItems");
  const cartItems = inCart && JSON.parse(inCart)

  const handleClearCart = () => {
    const confirmed = window.confirm('Are you sure you want to empty your cart?');
    if (confirmed) {
    //   dispatch(clearCart());
    localStorage.removeItem('cartItems');
    }
  };

  const handleIncreaseQuantity = (itemTitle: any) => {
    dispatch(addItemToCart({ title: itemTitle }));
  };
  const handleDecreaseQuantity = (itemTitle: any) => {
    dispatch(removeItemToCart({ title: itemTitle }));
  };
  const handleDeleteQuantity = (itemTitle: any) => {
    dispatch(removeItemFromCart({ itemTitle }));
  };

  return (
    <div>
      <div>
        <Header />
      </div>
      <h2>Cart</h2>
      {cartItems.length === 0 ? (
        <p>Your cart is empty.</p>
      ) : (
        <ul>
          {cartItems.map((item: CartItem) => (
            <li key={item.title}>
              <p>{item.title}</p>
              <p>Quantity: {item.quantity}</p>
              <button onClick={() => handleIncreaseQuantity(item.title)}>+</button>
              <button onClick={() => handleDecreaseQuantity(item.title)}>-</button>
              <button onClick={() => handleDeleteQuantity(item.title)}>Remove item</button>
            </li>
          ))}
        </ul>
      )}
      <button onClick={handleClearCart}>Clear Cart</button>
    </div>
  );
};

export default CartPage;
