using Allspark.Application.UseCases.Users.ResponseDtos;
using Allspark.Domain.Entities;

namespace Allspark.Infrastructure.Mappings;

public class GetUserByRefreshTokenProfile : Profile
{
    public GetUserByRefreshTokenProfile()
    {
        CreateMap<User, UserResponseDto>();
    }
}