using Allspark.Application.Enums;
using Allspark.Application.UseCases.Users.ResponseDtos;
using Allspark.Web.Services;

namespace Allspark.Web.Controllers.Auth;

public interface IAuthService
{
    public string CreateToken(UserResponseDto user, Roles myRole);
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    public RefreshTokenDto GenerateRefreshToken();
}