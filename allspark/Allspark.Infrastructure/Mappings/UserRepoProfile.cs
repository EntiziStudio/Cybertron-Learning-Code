using Allspark.Application.UseCases.Users.ResponseDtos;
using Allspark.Domain.Entities;

namespace Allspark.Infrastructure.Mappings
{
    public class UserRepoProfile : Profile
    {
        public UserRepoProfile()
        {
            CreateMap<User, UserResponseDto>();
        }
    }
}
