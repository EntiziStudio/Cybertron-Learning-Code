using Allspark.Application.UseCases.Users.ResponseDtos;
using Allspark.Domain.Entities;

namespace Allspark.Infrastructure.Mappings;

public class UpdateUserTokenProfile : Profile
{
    public UpdateUserTokenProfile()
    {
        CreateMap<User, UserResponseDto>();
    }
}