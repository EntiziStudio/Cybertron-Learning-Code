import * as types from "../types/cartTypes";

export const addToCart = (data) => ({
  type: types.ADD_TO_CART,
  data,
});

export const removeCart = (id) => ({
  type: types.REMOVE_CART,
  id,
});

export const resetCart = () => ({ type: types.RESET_CART });

export const getDiscount = (data) => ({
  type: types.GET_DISCOUNT,
  data,
});
