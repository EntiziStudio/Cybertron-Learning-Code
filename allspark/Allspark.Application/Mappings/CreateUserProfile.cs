using Allspark.Application.UseCases.Users.CreateUser;
using Allspark.Domain.Entities;

namespace Allspark.Application.Mappings;

public class CreateUserProfile : Profile
{
    public CreateUserProfile()
    {
        CreateMap<CreateUserCommand, User>();
    }
}