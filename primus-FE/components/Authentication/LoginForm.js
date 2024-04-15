import React from "react";
import axios from "axios";
import toast from "react-hot-toast";
import Router from "next/router";
import Link from "next/link";
import { useRouter } from "next/router";
import { motion } from "framer-motion";
import LoadingSpinner from "@/utils/LoadingSpinner";
import { allsparkUrl } from "@/utils/baseUrl";
import { handleLogin } from "@/utils/auth";

const INITIAL_USER = {
  email: "",
  password: "",
};

const LoginForm = () => {
  const [user, setUser] = React.useState(INITIAL_USER);
  const [disabled, setDisabled] = React.useState(true);
  const [loading, setLoading] = React.useState(false);
  const router = useRouter();

  React.useEffect(() => {
    const isUser = Object.values(user).every((el) => Boolean(el));
    isUser ? setDisabled(false) : setDisabled(true);
  }, [user]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUser((prevState) => ({ ...prevState, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      setLoading(true);
      const url = `${allsparkUrl}/api/auth/sign-in`;
      const payload = { ...user };
      const response = await axios.post(url, payload);
      
      handleLogin(response.data, router);

      toast.success("Đăng nhập thành công", {
        style: {
          border: "1px solid #4BB543",
          padding: "16px",
          color: "#4BB543",
        },
        iconTheme: {
          primary: "#4BB543",
          secondary: "#FFFAEE",
        },
      });
      Router.push("/");
    } catch (err) {
      toast.error("Sai email hoặc mật khẩu", {
        style: {
          border: "1px solid #ff0033",
          padding: "16px",
          color: "#ff0033",
        },
        iconTheme: {
          primary: "#ff0033",
          secondary: "#FFFAEE",
        },
      });
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
      <div className="login-form">
        <h2>ĐĂNG NHẬP</h2>

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Email</label>
            <input
              id="email"
              type="text"
              className="form-control"
              placeholder="Nhập email"
              name="email"
              value={user.email}
              onChange={handleChange}
            />
          </div>

          <div className="form-group">
            <label>Mật khẩu</label>
            <input
              id="password"
              type="password"
              className="form-control"
              placeholder="Nhập mật khẩu"
              name="password"
              value={user.password}
              onChange={handleChange}
            />
          </div>

          <div className="row align-items-center">
            <div className="col-lg-12 col-md-12 col-sm-12 remember-me-wrap">
              <Link href="/quen-mat-khau" className="lost-your-password">
                Quên mật khẩu
              </Link>
            </div>
          </div>

					<motion.button
						type="submit"
						disabled={disabled}
						whileTap={{ scale: 0.9 }}
					>
						Đăng nhập
						{loading ? <LoadingSpinner /> : ""}
					</motion.button>
        </form>
      </div>
    </>
  );
};

export default LoginForm;