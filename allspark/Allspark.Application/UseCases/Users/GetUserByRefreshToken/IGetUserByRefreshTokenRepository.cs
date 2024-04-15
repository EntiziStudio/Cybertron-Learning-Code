using Allspark.Application.UseCases.Users.ResponseDtos;

namespace Allspark.Application.UseCases.Users.GetUserByRefreshToken;

public interface IGetUserByRefreshTokenRepository
{
    Task<UserResponseDto> GetUserByRefreshTokenAsync(string refreshToken);
}