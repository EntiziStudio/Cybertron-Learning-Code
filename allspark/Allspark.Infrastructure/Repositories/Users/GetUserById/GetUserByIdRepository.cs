using Allspark.Application.UseCases.Users.GetUserById;
using Allspark.Application.UseCases.Users.ResponseDtos;
using Allspark.Domain.Entities;
using Allspark.Infrastructure.Data;

namespace Allspark.Infrastructure.Repositories.Users.GetUserById;

public class GetUserByIdRepository : IGetUserByIdRepository
{
    private readonly AllsparkDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetUserByIdRepository(AllsparkDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper;
    }

    public async Task<UserResponseDto> GetUserByIdAsync(int id)
    {
        var user = await _dbContext.Set<User>().FindAsync(id);

        return _mapper.Map<UserResponseDto>(user);
    }
}
