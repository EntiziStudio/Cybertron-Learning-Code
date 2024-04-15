using Allspark.Application.UseCases.Users.GetUserByRefreshToken;
using Allspark.Web.Extensions;

namespace Allspark.Web.Controllers.Auth.GetCurrentUser;

[Authorize]
[ApiController]
[Route("api/auth")]
[EnableCors(SecurityExtension.DefaultPolicy)]
public class GetCurrentUserController : AuthControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public GetCurrentUserController(IMediator mediator, IMapper mapper) : base(mediator)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("current-user")]
    public async Task<ActionResult<GetCurrentUserResponse>> CurrentUser()
    {
        var refreshToken = Request.Cookies[AuthConstants.RefreshTokenCookieKey];
        var user = await _mediator.Send(new GetUserByRefreshTokenQuery { RefreshToken = refreshToken! });

        if (user is null)
        {
            return Unauthorized(AuthConstants.UserNotFound);
        }

        if (user.TokenExpiresAt < DateTime.UtcNow)
        {
            return Unauthorized(AuthConstants.TokenExpired);
        }
        var response = _mapper.Map<GetCurrentUserResponse>(user);

        return Ok(response);
    }
}