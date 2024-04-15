using Allspark.Application.UseCases.Users.ResponseDtos;

namespace Allspark.Application.UseCases.Users.UpdateUserToken;

public interface IUpdateUserTokenRepository
{
    Task<UserResponseDto> UpdateUserTokenAsync(UpdateUserTokenRepoParam user);
}