import * as types from "../types/cartTypes";

const initialState = {
  cartItems: [],
  discount: 0.0,
};

const cartReducer = (state = initialState, action) => {
  switch (action.type) {
    case types.ADD_TO_CART:
      let existingItem = state.cartItems.find(
        (course) => action.data.id === course.id
      );
      if (existingItem) {
        existingItem.quantity += 1;
        return {
          ...state,
        };
      } else {
        return {
          ...state,
          cartItems: [...state.cartItems, action.data],
        };
      }

    case types.GET_DISCOUNT:
      return {
        ...state,
        discount: action.data,
      };

    case types.REMOVE_CART:
      let new_items = state.cartItems.filter(
        (item) => action.id !== item.id
      );
      return {
        ...state,
        cartItems: new_items,
      };
    case types.RESET_CART:
      return {
        ...state,
        cartItems: [],
      };
    default:
      return state;
  }
};

export default cartReducer;
