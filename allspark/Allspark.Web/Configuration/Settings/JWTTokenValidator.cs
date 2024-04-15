namespace Allspark.Web.Configuration.Settings;

public class JWTTokenValidator
{
    public static ClaimsPrincipal ValidateToken(string token, JWTSettings options)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Convert.FromBase64String(options.Key);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = options.Issuer,
            ValidAudience = options.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        try
        {
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (IsTokenValid(securityToken) && principal != null)
            {
                return principal;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Token validation failed: {ex.Message}");
        }

        return new ClaimsPrincipal();
    }

    private static bool IsTokenValid(SecurityToken securityToken)
    {
        return (securityToken.ValidTo > DateTime.UtcNow);
    }
}
