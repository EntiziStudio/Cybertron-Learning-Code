using Allspark.Application.UseCases.Users.UpdateUserToken;
using Allspark.Application.UseCases.Users.ResponseDtos;

namespace Allspark.Application.Mappings;

public class UpdateUserTokenProfile : Profile
{
    public UpdateUserTokenProfile()
    {
        CreateMap<UserResponseDto, UpdateUserTokenRepoParam>();
    }
}
