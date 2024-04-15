import React, { useEffect } from 'react';
import { Provider } from 'react-redux';
import axios from 'axios';
import cookie from 'js-cookie';
import { allsparkUrl } from '@/utils/baseUrl';
import '../styles/bootstrap.min.css';
import '../styles/animate.min.css';
import '../styles/boxicons.min.css';
import '../styles/flaticon.css';
import '../styles/nprogress.css';
// Global Styles
import '../styles/style.css';
import '../styles/responsive.css';

// Dashboard
import '../styles/dashboard.css';

import Layout from '@/components/_App/Layout';
import store from '@/store/index';

import {
  setCurrentUser,
  setAuthToken,
  setRefreshToken
} from '@/store/actions/authActions';

const PrimusApp = ({ Component, pageProps }) => {
  useEffect(() => {
    const initializeCurrentUser = async () => {
      let primus_auth_token = '';
      let primus_refresh_token = '';

      primus_auth_token = cookie.get('auth_token');
      primus_refresh_token = cookie.get('refresh_token');

      const authTokens = { primus_auth_token, primus_refresh_token };

      const storedCurrentUser = localStorage.getItem('currentUser');

      if (storedCurrentUser) {
        if (!authTokens.primus_auth_token) {
          localStorage.removeItem('currentUser');
          return;
        }
        const currentUser = JSON.parse(storedCurrentUser);
        store.dispatch(setCurrentUser(currentUser));
        store.dispatch(setAuthToken(authTokens.primus_auth_token));
        store.dispatch(setRefreshToken(authTokens.primus_refresh_token));
      } else {
        try {
          const options = {
            headers: {
              Authorization: `Bearer ${authTokens.primus_auth_token}`,
            },
            withCredentials: true,
          };

          const url = `${allsparkUrl}/api/auth/current-user`;
          const response = await axios.get(url, options);

          if (response.status === 401) {
            cookie.remove('auth_token');
            cookie.remove('refresh_token');
            localStorage.removeItem('currentUser');
            return;
          }

          store.dispatch(setCurrentUser(response.data));
          store.dispatch(setAuthToken(authTokens.primus_auth_token));
          store.dispatch(setRefreshToken(authTokens.primus_refresh_token));

          localStorage.setItem('currentUser', JSON.stringify(response.data));
        } catch (err) {
          cookie.remove('auth_token');
          cookie.remove('refresh_token');
          localStorage.removeItem('currentUser');
        }
      }
    };

    initializeCurrentUser();
  }, []);

  return (
    <Provider store={store}>
      <Layout>
        <Component {...pageProps} />
      </Layout>
    </Provider>
  );
}

export default PrimusApp;
