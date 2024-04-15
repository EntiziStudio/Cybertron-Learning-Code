using Allspark.Application.Enums;

namespace Allspark.Web.Controllers.Auth.SignUp;

public class SignUpResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Roles Role { get; set; }
}
