using Allspark.Application.UseCases.Users.ResponseDtos;
using Allspark.Web.Controllers.Auth.GetCurrentUser;

namespace Allspark.Web.Mappings;

public class GetCurrentUserProfile : Profile
{
    public GetCurrentUserProfile()
    {
        CreateMap<UserResponseDto, GetCurrentUserResponse>();
    }
}
