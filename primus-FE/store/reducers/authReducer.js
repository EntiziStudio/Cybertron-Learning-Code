import * as types from "../types/authTypes";

const initialState = {
	currentUser: null,
	authToken: null,
	refreshToken: null,
};

const authReducer = (state = initialState, action) => {
	switch (action.type) {
		case types.SET_CURRENT_USER:
			return {
				...state,
				currentUser: action.data,
			};

		case types.SET_AUTH_TOKEN:
			return {
				...state,
				authToken: action.data,
			};

		case types.SET_REFRESH_TOKEN:
			return {
				...state,
				refreshToken: action.data,
			};

		default:
			return state;
	}
};

export default authReducer;
