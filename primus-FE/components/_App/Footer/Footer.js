import React from "react";
import Link from "next/link";

const Footer = () => {
  const currentYear = new Date().getFullYear();
  return (
    <>
      <footer className="footer-area">
        <div className="container">
          <div className="row">
            <div className="col-lg-4 col-md-6 col-sm-6">
              <div className="single-footer-widget">
                <Link href="/">
                  <a className="logo">
                    <img src="/images/logo.png" alt="logo" />
                  </a>
                </Link>

                <p>
                  Working to bring significant changes in online-based learning
                  by doing extensive research for course curriculum preparation,
                  student engagements, and looking forward to the flexible
                  education!
                </p>

                <ul className="social-link">
                  <li>
                    <a
                      href="https://www.facebook.com/"
                      className="d-block"
                      target="_blank"
                      rel="noreferrer"
                      aria-label="Facebook"
                    >
                      <i className="bx bxl-facebook"></i>
                    </a>
                  </li>
                  <li>
                    <a
                      href="https://www.twitter.com/"
                      className="d-block"
                      target="_blank"
                      rel="noreferrer"
                      aria-label="Twitter"
                    >
                      <i className="bx bxl-twitter"></i>
                    </a>
                  </li>
                  <li>
                    <a
                      href="https://www.instagram.com/"
                      className="d-block"
                      target="_blank"
                      rel="noreferrer"
                      aria-label="Instagram"
                    >
                      <i className="bx bxl-instagram"></i>
                    </a>
                  </li>
                  <li>
                    <a
                      href="https://www.linkedin.com/"
                      className="d-block"
                      target="_blank"
                      rel="noreferrer"
                      aria-label="LinkedIn"
                    >
                      <i className="bx bxl-linkedin"></i>
                    </a>
                  </li>
                </ul>
              </div>
            </div>

            <div className="col-lg-2 col-md-6 col-sm-6 offset-lg-1">
              <div className="single-footer-widget">
                <h3>Khám phá</h3>
                <ul className="footer-links-list">
                  <li>
                    <Link href="/">
                      <a>Trang chủ</a>
                    </Link>
                  </li>
                  <li>
                    <Link href="/khoa-hoc">
                      <a>Khóa học</a>
                    </Link>
                  </li>
                  <li>
                    <Link href="/bai-tap">
                      <a>Bài tập</a>
                    </Link>
                  </li>
                  <li>
                    <Link href="/cuoc-thi">
                      <a>Cuộc thi</a>
                    </Link>
                  </li>
                </ul>
              </div>
            </div>

            <div className="col-lg-4 col-md-6 col-sm-6">
              <div className="single-footer-widget">
                <h3>Địa chỉ</h3>
                <ul className="footer-contact-info">
                  <li>
                    <i className="bx bx-map"></i>
                    Khu Công nghệ cao TP.HCM (SHTP), Xa lộ Hà Nội, P. Hiệp Phú,
                    TP. Thủ Đức, TP.HCM
                  </li>
                  <li>
                    <i className="bx bx-phone-call"></i>
                    <a href="tel:+44587154756">+1 (123) 456 7890</a>
                  </li>
                  <li>
                    <i className="bx bx-envelope"></i>
                    <a href="mailto:primus@gmail.com">primus@gmail.com</a>
                  </li>
                  <li>
                    <i className="bx bxs-inbox"></i>
                    <a href="tel:+557854578964">+55 785 4578964</a>
                  </li>
                </ul>
              </div>
            </div>
          </div>

          <div className="footer-bottom-area">
            <div className="row align-items-center">
              <div className="col-lg-6 col-md-6">
                <p>
                  <i className="bx bx-copyright"></i>
                  {currentYear} HUTECH
                </p>
              </div>

              <div className="col-lg-6 col-md-6">
                <ul>
                  <li>
                    <Link href="/privacy-policy">
                      <a>Privacy Policy</a>
                    </Link>
                  </li>
                  <li>
                    <Link href="/terms-conditions">
                      <a>Terms & Conditions</a>
                    </Link>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>

        <div className="lines">
          <div className="line"></div>
          <div className="line"></div>
          <div className="line"></div>
        </div>
      </footer>
    </>
  );
};

export default Footer;
