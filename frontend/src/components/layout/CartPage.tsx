import React from 'react'
import useAppDispatch from '../../hooks/useAppDispatch';
import useAppSelector from '../../hooks/useAppSelector';

const CartPage = () => {
    const dispatch = useAppDispatch();
    const { items } = useAppSelector((state) => state.cartReducer);

    
    return (
    <div>CartPage</div>
    )
}

export default CartPage