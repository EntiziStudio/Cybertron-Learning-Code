import Navbar from "@/components/_App/Navbar";
import PageBanner from "@/components/Common/PageBanner";
import ForgotPasswordForm from "@/components/Authentication/ForgotPasswordForm"

const ForgotPasswordPage = () => {
  return (
    <>
      <Navbar />

      <PageBanner
        pageTitle="QUÊN MẬT KHẨU"
        homePageUrl="/"
        homePageText="Trang chủ"
        activePageText="Quên mật khẩu"
      />

      <div className="profile-authentication-area ptb-100">
        <div className="container">
          <div className="row justify-content-center">
            <div className="col-lg-6 col-md-12">
              <ForgotPasswordForm />
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default ForgotPasswordPage;