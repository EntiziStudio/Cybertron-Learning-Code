import React from "react";
import { useRouter } from "next/router";
import { motion } from "framer-motion";

const INITIAL_USER = {
  email: "",
};

const ForgotPasswordForm = () => {
  const [user, setUser] = React.useState(INITIAL_USER);
  const [errorMessage, setErrorMessage] = React.useState('');
  const [successMessage, setSuccessMessage] = React.useState('');
  const router = useRouter();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUser((prevState) => ({ ...prevState, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch(`${baseUrl}/api/request_password_reset`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ user }),
      });

      if (response.ok) {
        setSuccessMessage(response.text());
        alert(successMessage);
        router.push("/login");
      } else {
        setErrorMessage(response.text());

      }
    } catch (error) {
      setErrorMessage('Đã xảy ra lỗi khi yêu cầu đặt lại mật khẩu.');
    }
  };

  return (
    <>
      <div className="login-form">
        <h2>QUÊN MẬT KHẨU</h2>

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Email</label>
            <input
              type="text"
              className="form-control"
              placeholder="Nhập Email"
              name="email"
              value={user.email}
              onChange={handleChange}
            />
          </div>
          {errorMessage && <p>{errorMessage}</p>}
          {successMessage && <p>{successMessage}</p>}
          <motion.button
            type="submit"
            whileTap={{ scale: 0.9 }}
          >
            Gửi
          </motion.button>
        </form>
      </div>
    </>
  );
};

export default ForgotPasswordForm;