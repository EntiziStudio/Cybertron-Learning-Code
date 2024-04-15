using Allspark.Application.UseCases.Users.ResponseDtos;

namespace Allspark.Application.UseCases.Users.GetUserByEmail;

public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, UserResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IGetUserByEmailRepository _getUserByEmailReponsitory;

    public GetUserByEmailHandler(IMapper mapper, IGetUserByEmailRepository getUserByEmailReponsitory)
    {
        _mapper = mapper;
        _getUserByEmailReponsitory = getUserByEmailReponsitory ?? throw new ArgumentNullException(nameof(getUserByEmailReponsitory));
    }

    public async Task<UserResponseDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _getUserByEmailReponsitory.GetUserByEmailAsync(request.Email);

        return user;
    }
}