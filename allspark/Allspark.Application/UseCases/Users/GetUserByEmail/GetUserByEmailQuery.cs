using Allspark.Application.UseCases.Users.ResponseDtos;

namespace Allspark.Application.UseCases.Users.GetUserByEmail;

public class GetUserByEmailQuery : IRequest<UserResponseDto>
{
    public string Email { get; set; } = string.Empty;
}