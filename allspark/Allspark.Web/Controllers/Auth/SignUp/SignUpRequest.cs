using Allspark.Application.Enums;

namespace Allspark.Web.Controllers.Auth.SignUp;

public class SignUpRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Roles Role { get; set; }
}
