using Allspark.Application.UseCases.Users.ResponseDtos;

namespace Allspark.Application.UseCases.Users.GetUserById;

public interface IGetUserByIdRepository
{
    Task<UserResponseDto> GetUserByIdAsync(int id);
}