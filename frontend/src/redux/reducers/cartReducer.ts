import { createSlice } from "@reduxjs/toolkit";
import { CartItem } from "../../types/CartItem";

interface CartReducer {
  items: CartItem[];
  count: number;
}

const initialState: CartReducer = {
  items: [],
  count: 0,
};

const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    addItemToCart: (state, action) => {
      const newItem: CartItem = action.payload;
      const inCart = localStorage.getItem('cartItems')
      const cartItems = inCart && JSON.parse(inCart)

      if(cartItems){
        cartItems.map((item: {title: String, quantity: number}) => {
          if(item.title === newItem.title){
            item.quantity += 1
          }
        });
        const existingItem = cartItems.find((item: {title: String}) => item.title === newItem.title);
        if (!existingItem) {
          const newCartItem = { ...newItem, quantity: 1 };
          cartItems.push(newCartItem);
        }
        console.log("cartItems: ", cartItems)
        localStorage.setItem('cartItems', JSON.stringify(cartItems))
        
        const updatedCart = localStorage.getItem('cartItems')
        const updatedCartItems = updatedCart && JSON.parse(updatedCart)
        state.items = [...updatedCartItems]

        // const existingItem = cartItems.find((item: {title: String}) => item.title === newItem.title);
        // if (existingItem) {
        //   if (existingItem.quantity){
        //     existingItem.quantity+= 1;
        //   } 
        //   console.log("existingItem: ", cartItems)
        // } else {
        //   const newCartItem = { ...newItem, quantity: 1 };
        //   state.items.push(newCartItem);
        //   localStorage.setItem('cartItems', JSON.stringify(state.items))
        //   // console.log("newCartItems: ", state.items)
        // }
      } else{
        const newCartItem = { ...newItem, quantity: 1 };
        const newCart = [newCartItem]
        // state.items.push(newCartItem);
        localStorage.setItem('cartItems', JSON.stringify(newCart))

        const updatedCart = localStorage.getItem('cartItems')
        const updatedCartItems = updatedCart && JSON.parse(updatedCart)
        state.items = [...updatedCartItems]

        console.log("newCartItems: ", state.items)
      }
    },
    removeItemToCart: (state, action) => {
      const newItem: CartItem = action.payload;
      const existingItem = state.items.find((item) => item.id === newItem.id);
      if (existingItem) {
        if (existingItem.quantity){
          existingItem.quantity-= 1;
        } 
      } else {
        const newCartItem = { ...newItem, quantity: 1 };
        state.items.push(newCartItem);
      }
    },
    removeItemFromCart: (state, action) => {
      const itemId: number = action.payload;
    const existingItemIndex = state.items.findIndex((item) => item.id === String(itemId));
      if (existingItemIndex !== -1) {
        const existingItem = state.items[existingItemIndex];
        if(existingItem.quantity){
        state.items.splice(existingItemIndex);
        }
      }
    },
    clearCart: (state) => {
      return initialState;
    },
  },
});

export const { addItemToCart, removeItemToCart, removeItemFromCart, clearCart } = cartSlice.actions;

const cartReducer = cartSlice.reducer;
export default cartReducer;
