using Allspark.Application.UseCases.Users.CreateUser;
using Allspark.Web.Extensions;

namespace Allspark.Web.Controllers.Auth.SignUp;

[ApiController]
[Route("api/auth")]
[EnableCors(SecurityExtension.DefaultPolicy)]
public class SignUpController : Controller
{
    private readonly IMediator _mediator;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public SignUpController(IMediator mediator, IMapper mapper, IAuthService authService)
    {
        _mediator = mediator;
        _authService = authService;
        _mapper = mapper;
    }

    [HttpPost("sign-up")]
    public async Task<ActionResult<SignUpResponse>> Register(SignUpRequest signUpRequest)
    {
        _authService.CreatePasswordHash(signUpRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var command = new CreateUserCommand()
        {
            FullName = signUpRequest.FullName,
            Email = signUpRequest.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = signUpRequest.Role
        };

        var response = await _mediator.Send(command);
        var result = _mapper.Map<SignUpResponse>(response);
        return Ok(result);
    }
}