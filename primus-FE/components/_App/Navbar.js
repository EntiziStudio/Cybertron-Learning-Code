import React from "react";
import Router from "next/router";
import NProgress from "nprogress";
import { motion } from "framer-motion";
import { useSelector } from "react-redux";
import ProfileDropdown from "./ProfileDropdown";
import SearchForm from "./SearchForm";
import ActiveLink from "@/utils/ActiveLink";

Router.onRouteChangeStart = () => NProgress.start();
Router.onRouteChangeComplete = () => NProgress.done();
Router.onRouteChangeError = () => NProgress.done();

const Navbar = () => {
  const [menu, setMenu] = React.useState(true);
  const user = useSelector((state) => state.auth.currentUser);
  const [isLoadingUser, setLoadingUser] = React.useState(true);

  const toggleNavbar = () => {
    setMenu(!menu);
  };

  React.useEffect(() => {
    let elementId = document.getElementById("navbar");
    document.addEventListener("scroll", () => {
      if (window.scrollY > 170) {
        elementId.classList.add("is-sticky");
      } else {
        elementId.classList.remove("is-sticky");
      }
    });

    const loadingTimeout = setTimeout(() => {
      setLoadingUser(false);
    }, 100);

    return () => clearTimeout(loadingTimeout); // Clear the timeout when the component unmounts
  }, []);

  const classOne = menu
    ? "collapse navbar-collapse"
    : "collapse navbar-collapse show";
  const classTwo = menu
    ? "navbar-toggler navbar-toggler-right collapsed"
    : "navbar-toggler navbar-toggler-right";

  return (
    <>
      <div id="navbar" className="navbar-area">
        <div className="edemy-nav">
          <div className="container-fluid">
            <div className="navbar navbar-expand-lg navbar-light">
              <ActiveLink href="/" activeClassName="active">
                <a className="navbar-brand" onClick={toggleNavbar}>
                  <img src="/images/logo_240x80.png" alt="logo" />
                </a>
              </ActiveLink>

              <button
                onClick={toggleNavbar}
                className={classTwo}
                type="button"
              >
                <span className="icon-bar top-bar"></span>
                <span className="icon-bar middle-bar"></span>
                <span className="icon-bar bottom-bar"></span>
              </button>

              <div className={classOne} id="navbarSupportedContent">
                <SearchForm />

                <ul className="navbar-nav">
                  <motion.li
                    className="nav-item"
                    whileHover={{
                      scale: 1.1,
                      transition: { duration: 0.5 },
                    }}
                    whileTap={{ scale: 0.9 }}
                  >
                    <ActiveLink href="/" activeClassName="active">
                      <a onClick={toggleNavbar} className="nav-link">
                        Trang chủ
                      </a>
                    </ActiveLink>
                  </motion.li>

                  <motion.li
                    className="nav-item"
                    whileHover={{
                      scale: 1.1,
                      transition: { duration: 0.5 },
                    }}
                    whileTap={{ scale: 0.9 }}
                  >
                    <ActiveLink href="/khoa-hoc" activeClassName="active">
                      <a onClick={toggleNavbar} className="nav-link">
                        Khoá học
                      </a>
                    </ActiveLink>
                  </motion.li>
                  <motion.li
                    className="nav-item"
                    whileHover={{
                      scale: 1.1,
                      transition: { duration: 0.5 },
                    }}
                    whileTap={{ scale: 0.9 }}
                  >
                    <ActiveLink href="/bai-tap" activeClassName="active">
                      <a onClick={toggleNavbar} className="nav-link">
                        Bài tập
                      </a>
                    </ActiveLink>
                  </motion.li>
                  <motion.li
                    className="nav-item"
                    whileHover={{
                      scale: 1.1,
                      transition: { duration: 0.5 },
                    }}
                    whileTap={{ scale: 0.9 }}
                  >
                    <ActiveLink href="/cuoc-thi" activeClassName="active">
                      <a onClick={toggleNavbar} className="nav-link">
                        Cuộc thi
                      </a>
                    </ActiveLink>
                  </motion.li>
                </ul>
              </div>

              <div className="others-option d-flex align-items-center">
                <div className="option-item">
                  {/* Only show the login button if the user data is not loading */}
                  {!isLoadingUser && (
                    <>
                      {user ? (
                        <ProfileDropdown {...user} />
                      ) : (
                        <ActiveLink href="/dang-nhap" activeClassName="active">
                          <a className="default-btn">
                            <i className="flaticon-user"></i> Đăng nhập{" "}
                            <span></span>
                          </a>
                        </ActiveLink>
                      )}
                    </>
                  )}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default Navbar;
