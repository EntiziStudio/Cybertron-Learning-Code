using Allspark.Application.UseCases.Users.ResponseDtos;
using Allspark.Domain.Entities;

namespace Allspark.Application.UseCases.Users.CreateUser;

public interface ICreateUserRepository
{
    Task<UserResponseDto> CreateUserAsync(User user);
}