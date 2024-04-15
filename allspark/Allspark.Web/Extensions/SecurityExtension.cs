using Allspark.Application.Wrappers;
using Allspark.Web.Controllers.Auth;
using Allspark.Web.Services;
using Allspark.Web.Configuration.Settings;
using Allspark.Web.Configuration;

namespace Allspark.Web.Extensions;

public static class SecurityExtension
{
    public const string DefaultPolicy = "DEFAULT_POLICY";

    public static void AddAuthService(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
    }

    public static void AddAllsparkCors(this IServiceCollection services)
    {
        services.AddCors(option =>
        {
            option.AddPolicy(DefaultPolicy, builder =>
            {
                builder.AllowAnyHeader().AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:2023");
            });
        });

    }

    public static void AddAllsparkOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JWTSettings>(configuration.GetSection(ConfigurationConstants.JwtConfigurationName));
    }

    public static void AddAllsparkJWT(this IServiceCollection services, IConfiguration configuration)
    {
        var secutityOption = new JWTSettings();
        configuration.GetSection(ConfigurationConstants.JwtConfigurationName).Bind(secutityOption);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var cle = secutityOption.Key;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(cle)),
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateActor = false,
                ValidateLifetime = true,

            };
            options.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = c =>
                {
                    c.NoResult();
                    c.Response.StatusCode = 500;
                    c.Response.ContentType = "text/plain";
                    return c.Response.WriteAsync(c.Exception.ToString());
                },
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    var result = JsonConvert.SerializeObject(new Response<string>(AuthConstants.UnAuthorized));
                    return context.Response.WriteAsync(result);
                },
                OnForbidden = context =>
                {
                    context.Response.StatusCode = 403;
                    context.Response.ContentType = "application/json";
                    var result = JsonConvert.SerializeObject(new Response<string>(AuthConstants.UnAuthorizedToAccessResource));
                    return context.Response.WriteAsync(result);
                },
            };
        });
    }
}