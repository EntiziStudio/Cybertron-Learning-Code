using Allspark.Application.UseCases.Users.GetUserByEmail;
using Allspark.Application.UseCases.Users.ResponseDtos;
using Allspark.Domain.Entities;
using Allspark.Infrastructure.Data;

namespace Allspark.Infrastructure.Repositories.Users.GetUserByEmail;

public class GetUserByEmailRepository : IGetUserByEmailRepository
{
    private readonly AllsparkDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetUserByEmailRepository(AllsparkDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper;
    }

    public async Task<UserResponseDto> GetUserByEmailAsync(string email)
    {
        var user = await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == email);
        return _mapper.Map<UserResponseDto>(user);
    }
}