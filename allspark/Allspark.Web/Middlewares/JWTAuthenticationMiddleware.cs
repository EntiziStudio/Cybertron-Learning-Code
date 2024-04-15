using Allspark.Web.Configuration.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Allspark.Web.Middlewares;

public class JWTAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JWTSettings _options;

    public JWTAuthenticationMiddleware(
        RequestDelegate next,
        IOptions<JWTSettings> options)
    {
        _next = next;
        _options = options.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out StringValues authHeader))
        {
            await _next(context);
            return;
        }

        string token = authHeader.FirstOrDefault()?.Split(" ").Last();
        if (token != null)
        {
            var principal = JWTTokenValidator.ValidateToken(token, _options);
            if (principal != null)
            {
                context.User = principal;
            }
        }

        await _next(context);
    }
}
