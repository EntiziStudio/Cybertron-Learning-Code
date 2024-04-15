using Allspark.Application.UseCases.Users.ResponseDtos;

namespace Allspark.Application.UseCases.Users.GetUserByRefreshToken;

public class GetUserByRefreshTokenQuery : IRequest<UserResponseDto>
{
    public string RefreshToken { get; set; } = string.Empty;
}