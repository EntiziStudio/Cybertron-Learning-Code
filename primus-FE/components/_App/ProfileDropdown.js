import React from "react";
import Link from "next/link";
import { useRouter } from "next/router";
import cookie from "js-cookie";

const ProfileDropdown = ({ username }) => {
	const router = useRouter();
	const handleLogout = () => {
    cookie.remove('auth_token');
    cookie.remove('refresh_token');
    localStorage.removeItem("primus_auth_token");
    localStorage.removeItem("primus_refresh_token");
		router.reload("/");
	}

	return (
		<div className="dropdown profile-dropdown">
			<div className="img ptb-15">
				<img src="/images/avatar.jpg" alt={username} />
			</div>
			<ul className="dropdown-menu">
				<li>
					{/* <Link href="/" className="dropdown-item author-dropdown-item">
						<div className="d-flex align-items-center">
							<div className="img">
								<img
									src="/images/avatar.jpg"
									alt={username}
								/>
							</div>
							<span className="ps-3">
								<span className="fw-semibold fs-16 mb-1 d-block">
									{username}
								</span>
							</span>
						</div>
					</Link> */}
				</li>
				<li>
					<hr className="dropdown-divider" />
				</li>

				<li>
					{/* <Link href="/" className="dropdown-item">
						<i className="bx bx-book"></i>
						Giấy chứng nhận
					</Link> */}
				</li>
				<li>
					{/* <Link href="/" className="dropdown-item">
						<i className="bx bx-credit-card-front"></i>
						Cài đặt
					</Link> */}
				</li>
				<li>
					<hr className="dropdown-divider" />
				</li>
				<li>
					<button
						type="submit"
						onClick={handleLogout}
						className="dropdown-item"
					>
						<i className="bx bx-log-out"></i> Đăng xuất
					</button>
				</li>
			</ul>
		</div>
	);
};

export default ProfileDropdown;
