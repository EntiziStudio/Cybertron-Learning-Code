using Allspark.Application.UseCases.Users.GetUserByEmail;
using Allspark.Web.Extensions;

namespace Allspark.Web.Controllers.Auth.SignIn;

[ApiController]
[Route("api/auth")]
[EnableCors(SecurityExtension.DefaultPolicy)]
public class SignInController : AuthControllerBase
{
    private readonly IMediator _mediator;
    private readonly IAuthService _authService;
    public SignInController(IMediator mediator, IAuthService authService) : base(mediator)
    {
        _mediator = mediator;
        _authService = authService;
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult<SignInResponse>> SignInAsync(SignInRequest signInDto)
    {
        var user = await _mediator.Send(new GetUserByEmailQuery() { Email = signInDto.Email });
        // FACT: this flow may have a gap to check registered email
        if (user is null)
        {
            return BadRequest(AuthConstants.UserNotFound);
        }

        if (!_authService.VerifyPasswordHash(signInDto.Password, user.PasswordHash!, user.PasswordSalt!))
        {
            return BadRequest(AuthConstants.WrongPassword);
        }

        var token = _authService.CreateToken(user, user.Role);

        var refreshToken = _authService.GenerateRefreshToken();
        await SetRefreshTokenAsync(user, refreshToken);

        var response = new SignInResponse
        {
            AccessToken = token,
            RefreshToken = refreshToken.Token
        };

        return Ok(response);
    }
}