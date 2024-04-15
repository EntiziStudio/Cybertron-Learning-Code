using Allspark.Application.UseCases.Users.ResponseDtos;
using Allspark.Application.UseCases.Users.UpdateUserToken;
using Allspark.Domain.Entities;
using Allspark.Infrastructure.Data;

namespace Allspark.Infrastructure.Repositories.Users.UpdateUserToken;

public class UpdateUserTokenRepository : IUpdateUserTokenRepository
{
    private readonly AllsparkDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateUserTokenRepository(AllsparkDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper;
    }
    public async Task<UserResponseDto> UpdateUserTokenAsync(UpdateUserTokenRepoParam user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var existingUser = await _dbContext.Set<User>().FirstOrDefaultAsync(p => p.Id == user.Id);

        if (existingUser != null)
        {
            _dbContext.Entry(existingUser).CurrentValues.SetValues(user);
            int affectedRows = await _dbContext.SaveChangesAsync();
            if (affectedRows > 0)
            {
                return _mapper.Map<UserResponseDto>(existingUser);
            }
        }

        throw new InvalidOperationException($"User with id {user.Id} not found in {nameof(UpdateUserTokenRepository)}");
    }
}
