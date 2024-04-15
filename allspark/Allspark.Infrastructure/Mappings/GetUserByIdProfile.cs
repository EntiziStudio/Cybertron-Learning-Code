using Allspark.Application.UseCases.Users.ResponseDtos;
using Allspark.Domain.Entities;

namespace Allspark.Infrastructure.Mappings;

public class GetUserByIdProfile : Profile
{
    public GetUserByIdProfile()
    {
        CreateMap<User, UserResponseDto>();
    }
}