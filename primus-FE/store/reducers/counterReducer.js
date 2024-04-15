import * as types from "../types/counterTypes";

const initialState = {
  count: 0,
};

const counterReducer = (state = initialState, action) => {
  switch (action.type) {
    case types.INCREMENT:
      return {
        ...state,
        count: state.count + 1,
      };
    case types.DECREMENT:
      return {
        ...state,
        count: state.count - 1,
      };
    case types.RESET:
      return {
        ...state,
        count: 0,
      };
    default:
      return state;
  }
};

export default counterReducer;
