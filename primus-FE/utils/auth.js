import Router from "next/router";
import cookie from "js-cookie";
import axios from 'axios';
import { allsparkUrl } from '@/utils/baseUrl';
import store from '@/store/index';
import { setCurrentUser, setAuthToken, setRefreshToken } from '@/store/actions/authActions';


const AUTH_TOKEN_COOKIE = 'auth_token';
const REFRESH_TOKEN_COOKIE = 'refresh_token';

export const setCookieAuthToken = (token) => {
  cookie.set(AUTH_TOKEN_COOKIE, token);
};

export const setCookieRefreshToken = (token) => {
  cookie.set(REFRESH_TOKEN_COOKIE, token);
};

export const removeCookieTokens = () => {
  cookie.remove(AUTH_TOKEN_COOKIE);
  cookie.remove(REFRESH_TOKEN_COOKIE);
};

export const handleLogin = async (data, routeNext) => {
  setCookieAuthToken(data.accessToken);
  setCookieRefreshToken(data.refreshToken);

  const options = {
    headers: {
      Authorization: `Bearer ${data.accessToken}`,
    },
    withCredentials: true,
  };

  const url = `${allsparkUrl}/api/auth/current-user`;
  const response = await axios.get(url, options);

  if (response.status === 401) {
    removeCookieTokens();
    localStorage.removeItem('currentUser');
    return;
  }

  store.dispatch(setCurrentUser(response.data));
  store.dispatch(setAuthToken(data.accessToken));
  store.dispatch(setRefreshToken(data.refreshToken));

  localStorage.setItem('currentUser', JSON.stringify(response.data));

  if (routeNext.query && routeNext.query.next) {
    Router.push(routeNext.query.next);
  } else {
    Router.push("/");
  }
};

export const handleLogout = () => {
  removeCookieTokens();
  localStorage.removeItem('currentUser');

  Router.push("/");
};

export const destroyToken = () => {
  removeCookieTokens();
  Router.reload("/");
};

export const redirectUser = (ctx, location) => {
  if (ctx.req) {
    ctx.res.writeHead(302, { Location: location });
    ctx.res.end();
  } else {
    Router.push({ pathname: location, query: { next: ctx.pathname } });
  }
};

export const slugify = (string) => {
  return string
    .toString()
    .trim()
    .toLowerCase()
    .replace(/\s+/g, "-")
    .replace(/[^\w\-]+/g, "")
    .replace(/\-\-+/g, "-")
    .replace(/^-+/, "")
    .replace(/-+$/, "");
};