using Allspark.Application.UseCases.Users.ResponseDtos;
using Allspark.Domain.Entities;

namespace Allspark.Infrastructure.Mappings;

public class GetUserByEmailProfile : Profile
{
    public GetUserByEmailProfile()
    {
        CreateMap<User, UserResponseDto>();
    }
}