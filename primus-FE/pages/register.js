import React from "react";
import RegisterForm from "@/components/Authentication/RegisterForm";


const RegisternPage = () => {
  return (
    <>
      <div className="profile-authentication-area ptb-100">
        <div className="container">
          <div className="row justify-content-center">
            <div className="col-lg-6 col-md-12">
              <RegisterForm />
            </div>
          </div>
        </div>
      </div>
    </>
  )
};

export default RegisternPage;