using Allspark.Application.UseCases.Users.ResponseDtos;

namespace Allspark.Application.UseCases.Users.UpdateUserToken;

public class UpdateUserTokenCommand : IRequest<UserResponseDto>
{
    public int UserId { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? TokenCreatedAt { get; set; }
    public DateTime? TokenExpiresAt { get; set; }
}