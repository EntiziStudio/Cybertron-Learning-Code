using Allspark.Application.UseCases.Users.GetUserByRefreshToken;
using Allspark.Application.UseCases.Users.ResponseDtos;
using Allspark.Domain.Entities;
using Allspark.Infrastructure.Data;

namespace Allspark.Infrastructure.Repositories.Users.GetUserByRefreshToken;

public class GetUserByRefreshTokenRepository : IGetUserByRefreshTokenRepository
{
    private readonly AllsparkDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetUserByRefreshTokenRepository(AllsparkDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper;
    }

    public async Task<UserResponseDto> GetUserByRefreshTokenAsync(string refreshToken)
    {
        var user = await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        return _mapper.Map<UserResponseDto>(user);
    }
}
