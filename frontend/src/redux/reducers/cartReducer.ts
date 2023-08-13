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
      const existingItem = state.items.find((item) => item.id === newItem.id);
      if (existingItem) {
        if (existingItem.quantity){
          existingItem.quantity+= 1;
        } 
      } else {
        const newCartItem = { ...newItem, quantity: 1 };
        state.items.push(newCartItem);
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
