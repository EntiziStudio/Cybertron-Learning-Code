import React from "react";
import axios from "axios";
import toast from "react-hot-toast";
import Router from "next/router";
import { motion } from "framer-motion";
import LoadingSpinner from "@/utils/LoadingSpinner";
import { allsparkUrl } from "@/utils/baseUrl";

const INITIAL_USER = {
  fullName: "",
  email: "",
  password: "",
  confirmPassword: "",
};

const RegisterForm = () => {
  const [user, setUser] = React.useState(INITIAL_USER);
  const [disabled, setDisabled] = React.useState(true);
  const [loading, setLoading] = React.useState(false);

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
      const url = `${allsparkUrl}/api/auth/sign-up`;
      const payload = { ...user };
      const response = await axios.post(url, payload);
      toast.success("Đăng ký thành công", {
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
    } catch (error) {
      let {
        response: {
          data: { message },
        },
      } = error;
      toast.error(message, {
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
      <div className="register-form">
        <h2>ĐĂNG KÝ</h2>

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Họ và tên</label>
            <input
              type="text"
              className="form-control"
              placeholder="Nhập họ tên"
              name="fullName"
              value={user.fullName}
              onChange={handleChange}
            />
          </div>

          <div className="form-group">
            <label>Email</label>
            <input
              type="email"
              className="form-control"
              placeholder="Nhập email"
              name="email"
              value={user.email}
              onChange={handleChange}
            />
          </div>

          <div className="form-group">
            <label>Mật khẩu</label>
            <input
              type="password"
              className="form-control"
              placeholder="Nhập mật khẩu"
              name="password"
              value={user.password}
              onChange={handleChange}
            />
          </div>

          <div className="form-group">
            <label>Nhập lại mật khẩu</label>
            <input
              type="password"
              className="form-control"
              placeholder="Nhập lại mật khẩu"
              name="confirmPassword"
              value={user.confirmPassword}
              onChange={handleChange}
            />
          </div>

          <motion.button
            type="submit"
            disabled={disabled}
            whileTap={{ scale: 0.9 }}
          >
            Đăng ký
            {loading ? <LoadingSpinner /> : ""}
          </motion.button>
        </form>
      </div>
    </>
  );
};

export default RegisterForm;
