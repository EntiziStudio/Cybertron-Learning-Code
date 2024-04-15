using Allspark.Application.UseCases.Users.ResponseDtos;
using Allspark.Domain.Entities;

namespace Allspark.Infrastructure.Mappings;

public class CreateUserProfile : Profile
{
    public CreateUserProfile()
    {
        CreateMap<User, UserResponseDto>();
    }
}