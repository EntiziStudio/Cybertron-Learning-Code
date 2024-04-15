import * as types from "../types/authTypes";

export const setCurrentUser = (data) => ({
  type: types.SET_CURRENT_USER,
  data,
});

export const setAuthToken = (data) => ({
  type: types.SET_AUTH_TOKEN,
  data,
});

export const setRefreshToken = (data) => ({
  type: types.SET_REFRESH_TOKEN,
  data,
});
