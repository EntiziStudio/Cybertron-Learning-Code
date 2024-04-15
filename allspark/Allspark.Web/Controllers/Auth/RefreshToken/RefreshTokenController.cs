using Allspark.Application.UseCases.Users.GetUserByRefreshToken;

using Allspark.Web.Extensions;

namespace Allspark.Web.Controllers.Auth.RefreshToken;

[ApiController]
[Route("api/auth")]
[EnableCors(SecurityExtension.DefaultPolicy)]
public class RefreshTokenController : AuthControllerBase
{
    private readonly IMediator _mediator;
    private readonly IAuthService _authService;
    public RefreshTokenController(IMediator mediator, IAuthService authService) : base(mediator)
    {
        _mediator = mediator;
        _authService = authService;
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest refreshTokenRequest)
    {
        var user = await _mediator.Send(new GetUserByRefreshTokenQuery { RefreshToken = refreshTokenRequest.RefreshToken });

        if (user is null)

        {
            return Unauthorized(AuthConstants.UserNotFound);
        }

        if (user.TokenExpiresAt < DateTime.UtcNow)
        {
            return Unauthorized(AuthConstants.TokenExpired);
        }

        var token = _authService.CreateToken(user, user.Role);
        var newRefreshToken = _authService.GenerateRefreshToken();
        await SetRefreshTokenAsync(user, newRefreshToken);

        var response = new RefreshTokenResponse
        {
            AccessToken = token,
            RefreshToken = newRefreshToken.Token
        };

        return Ok(response);
    }
}
