import React, { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { useRouter } from 'next/router';
import { redirectUser } from '@/utils/auth';

const withAuth = (WrappedComponent) => {
  const Wrapper = (props) => {
    const router = useRouter();
    const currentUser = useSelector((state) => state.auth.currentUser);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
      if (typeof window !== 'undefined' && !currentUser) {
        redirectUser(router, '/dang-nhap');
      } else {
        setLoading(false);
      }
    }, []);

    if (loading) {
      return <div>Loading...</div>;
    }

    return <WrappedComponent {...props} />;
  };

  Wrapper.displayName = `withAuth(${WrappedComponent.displayName || WrappedComponent.name || 'Component'})`;

  return Wrapper;
};

export default withAuth;
