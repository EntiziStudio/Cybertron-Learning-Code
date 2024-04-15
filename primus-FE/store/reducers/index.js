import { combineReducers } from 'redux';
import cartReducer from './cartReducer';
import authReducer from './authReducer';
import counterReducer from './counterReducer';

const rootReducer = combineReducers({
  cart: cartReducer,
  auth: authReducer,
  counter: counterReducer,
});

export default rootReducer;
