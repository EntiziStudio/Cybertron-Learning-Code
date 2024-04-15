namespace Allspark.Application.UseCases.Users.UpdateUserToken;

public class UpdateUserTokenRepoParam
{
    public int Id { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? TokenCreatedAt { get; set; }
    public DateTime? TokenExpiresAt { get; set; }
}