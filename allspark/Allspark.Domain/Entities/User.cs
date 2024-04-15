namespace Allspark.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Role { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime? TokenCreatedAt { get; set; }
        public DateTime? TokenExpiresAt { get; set; }
    }
}
