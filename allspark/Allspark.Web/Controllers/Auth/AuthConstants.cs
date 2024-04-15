namespace Allspark.Web.Controllers.Auth;

public class AuthConstants
{
    public const string EmailNotEmpty = "Email không được để trống";
    public const string EmailInValid = "Email không hợp lệ";
    public const string PasswordNotEmpty = "Mật khẩu không được để trống";

    public const string UserNotFound = "Không tìm thấy tài khoản";
    public const string UserExisted = "Tài khoản đã tồn tại";
    public const string WrongPassword = "Sai mật khẩu";

    public const string InvalidRefreshToken = "RefreshToken không hợp lệ";
    public const string UnAuthorized = "Bạn chưa xác thực";
    public const string UnAuthorizedToAccessResource = "Bạn không có quyền để truy cập tài nguyên này";
    public const string TokenExpired = "Token hết hạn";

    public const string RefreshTokenCookieKey = "refresh_token";
}