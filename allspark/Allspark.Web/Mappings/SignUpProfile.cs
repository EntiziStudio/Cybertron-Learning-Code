using Allspark.Application.UseCases.Users.ResponseDtos;
using Allspark.Web.Controllers.Auth.SignUp;

namespace Allspark.Web.Mappings;

public class SignUpProfile : Profile
{
    public SignUpProfile()
    {
        CreateMap<UserResponseDto, SignUpResponse>();
    }
}