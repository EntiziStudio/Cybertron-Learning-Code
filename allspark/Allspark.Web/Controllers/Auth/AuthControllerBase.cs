using Allspark.Application.UseCases.Users.ResponseDtos;
using Allspark.Application.UseCases.Users.UpdateUserToken;
using Allspark.Web.Services;

namespace Allspark.Web.Controllers.Auth;

public class AuthControllerBase : Controller
{
    private readonly IMediator _mediator;
    public AuthControllerBase(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected async Task SetRefreshTokenAsync(UserResponseDto userDto, RefreshTokenDto newRefreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = newRefreshToken.ExpiresAt
        };
        
        Response.Cookies.Append(AuthConstants.RefreshTokenCookieKey, newRefreshToken.Token, cookieOptions);

        await _mediator.Send(new UpdateUserTokenCommand()
        {
            UserId = userDto.Id,
            RefreshToken = newRefreshToken.Token,
            TokenCreatedAt = newRefreshToken.CreatedAt,
            TokenExpiresAt = newRefreshToken.ExpiresAt
        });
    }
}
