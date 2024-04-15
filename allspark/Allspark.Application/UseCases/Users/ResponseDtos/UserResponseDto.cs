using Allspark.Application.Enums;

namespace Allspark.Application.UseCases.Users.ResponseDtos;

public class UserResponseDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Roles Role { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string? RefreshToken { get; set; } = string.Empty;
    public DateTime? TokenCreatedAt { get; set; }
    public DateTime? TokenExpiresAt { get; set; }
}
