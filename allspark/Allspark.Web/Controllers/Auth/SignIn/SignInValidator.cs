namespace Allspark.Web.Controllers.Auth.SignIn;

public class SignInValidator : AbstractValidator<SignInRequest>
{
    public SignInValidator()
    {
        RuleFor(signInDto => signInDto.Email)
            .NotEmpty().WithMessage(AuthConstants.EmailNotEmpty)
            .EmailAddress().WithMessage(AuthConstants.EmailInValid);
        RuleFor(model => model.Password)
            .NotEmpty().WithMessage(AuthConstants.PasswordNotEmpty);
    }
}