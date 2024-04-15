using Allspark.Application.UseCases.Users.CreateUser;
using Allspark.Application.UseCases.Users.ResponseDtos;
using Allspark.Domain.Entities;
using Allspark.Infrastructure.Data;

namespace Allspark.Infrastructure.Repositories.Users.CreateUser;

public class CreateUserRepository : ICreateUserRepository
{
    private readonly AllsparkDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateUserRepository(AllsparkDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper;
    }
    public async Task<UserResponseDto> CreateUserAsync(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        await _dbContext.Set<User>().AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<UserResponseDto>(user);
    }
}
