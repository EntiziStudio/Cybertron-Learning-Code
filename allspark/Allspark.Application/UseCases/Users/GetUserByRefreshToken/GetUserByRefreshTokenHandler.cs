using Allspark.Application.UseCases.Users.ResponseDtos;

namespace Allspark.Application.UseCases.Users.GetUserByRefreshToken;

public class GetUserByRefreshTokenHandler : IRequestHandler<GetUserByRefreshTokenQuery, UserResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IGetUserByRefreshTokenRepository _getUserByRefreshTokenRepository;

    public GetUserByRefreshTokenHandler(IMapper mapper, IGetUserByRefreshTokenRepository getUserByRefreshTokenRepository)
    {
        _mapper = mapper;
        _getUserByRefreshTokenRepository = getUserByRefreshTokenRepository ?? throw new ArgumentNullException(nameof(getUserByRefreshTokenRepository));
    }
    public async Task<UserResponseDto> Handle(GetUserByRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _getUserByRefreshTokenRepository.GetUserByRefreshTokenAsync(request.RefreshToken);

        return user;
    }
}