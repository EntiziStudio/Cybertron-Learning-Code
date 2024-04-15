using Allspark.Application.UseCases.Users.GetUserById;
using Allspark.Application.UseCases.Users.ResponseDtos;

namespace Allspark.Application.UseCases.Users.UpdateUserToken
{
    public class UpdateUserTokenHandler : IRequestHandler<UpdateUserTokenCommand, UserResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IGetUserByIdRepository _getUserByIdRepository;
    private readonly IUpdateUserTokenRepository _updateUserTokenRepository;

    public UpdateUserTokenHandler(IMapper mapper, IGetUserByIdRepository getUserByIdRepository, IUpdateUserTokenRepository updateUserTokenRepository)
    {
        _mapper = mapper;
        _getUserByIdRepository = getUserByIdRepository;
        _updateUserTokenRepository = updateUserTokenRepository;
    }

    public async Task<UserResponseDto> Handle(UpdateUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _getUserByIdRepository.GetUserByIdAsync(request.UserId);
        if (user != null)
        {
            user.RefreshToken = request.RefreshToken;
            user.TokenCreatedAt = request.TokenCreatedAt;
            user.TokenExpiresAt = request.TokenExpiresAt;

            var param = _mapper.Map<UpdateUserTokenRepoParam>(user);
            var result = await _updateUserTokenRepository.UpdateUserTokenAsync(param);

            return result;
        }

        throw new ValidationException("User not found");
    }
}
}
