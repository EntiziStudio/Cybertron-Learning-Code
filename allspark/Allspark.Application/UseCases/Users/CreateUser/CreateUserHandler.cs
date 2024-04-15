using Allspark.Application.UseCases.Users.GetUserByEmail;
using Allspark.Application.UseCases.Users.ResponseDtos;
using Allspark.Domain.Entities;
using Allspark.Application.Exceptions;

namespace Allspark.Application.UseCases.Users.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IGetUserByEmailRepository _getUserByEmailRepository;
    private readonly ICreateUserRepository _createUserRepository;

    public CreateUserHandler(
        IMapper mapper,
        IGetUserByEmailRepository getUserByEmailRepository,
        ICreateUserRepository createUserRepository)
    {
        _mapper = mapper;
        _getUserByEmailRepository = getUserByEmailRepository;
        _createUserRepository = createUserRepository;
    }

    public async Task<UserResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _getUserByEmailRepository.GetUserByEmailAsync(request.Email);

        if (string.IsNullOrEmpty(request.Email))
        {
            throw new AllsparkValidationException(CreateUserConstants.EmailNotEmpty);
        }
        if (existingUser is not null)
        {
            throw new AllsparkValidationException(CreateUserConstants.UserAlreadyExists);
        }

        var user = _mapper.Map<User>(request);
        var result = await _createUserRepository.CreateUserAsync(user);

        return result;
    }
}