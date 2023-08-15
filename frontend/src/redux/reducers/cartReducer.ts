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
        // console.log("cartItems: ", cartItems)
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
    decreaseItemInCart: (state, action) => {
      const newItem: CartItem = action.payload;
      const existingItem = state.items.find((item) => item.title === newItem.title);
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
      const itemTitle = action.payload;
      const updatedCart = localStorage.getItem('cartItems')
      const updatedCartItems = updatedCart && JSON.parse(updatedCart)
    const existingItemIndex = updatedCartItems.findIndex((item: {title: String}) => item.title === itemTitle.itemTitle);
      if (existingItemIndex !== -1) {
        // const existingItem = updatedCartItems[existingItemIndex];
        // if(existingItem.quantity){
        // }
        updatedCartItems.splice(existingItemIndex, 1);
        state.items = [...updatedCartItems]
        localStorage.setItem('cartItems', JSON.stringify(state.items))
      }
    },
    clearCart: (state) => {
      return initialState;
    },
  },
});

export const { addItemToCart, decreaseItemInCart, removeItemFromCart, clearCart } = cartSlice.actions;

const cartReducer = cartSlice.reducer;
export default cartReducer;
