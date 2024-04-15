import React from 'react'
import Link from 'next/link'

const Custom404 = () => {
  return (
    <>
      <div className="error-area">
        <div className="d-table">
          <div className="d-table-cell">
            <div className="container">
              <div className="error-content">
                <img src="/images/error.png" alt="image" />
                <h3>Lỗi 404: Không tìm thấy trang</h3>
                <p>
                  Trang bạn đang tìm có thể đã bị xóa, tên trang đã bị thay đổi hoặc tạm thời không khả dụng.
                </p>

                <div className="btn-box">
                  <Link href="/">
                    <a className="default-btn">
                      <i className="flaticon-history"></i> Trở lại <span></span>
                    </a>
                  </Link>
                  <Link href="/">
                    <a className="default-btn">
                      <i className="flaticon-home"></i> Trang chủ <span></span>
                    </a>
                  </Link>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  )
}

export default Custom404
