using Allspark.Application.UseCases.Users.ResponseDtos;

namespace Allspark.Application.UseCases.Users.GetUserByEmail;

public interface IGetUserByEmailRepository
{
    Task<UserResponseDto> GetUserByEmailAsync(string email);
}