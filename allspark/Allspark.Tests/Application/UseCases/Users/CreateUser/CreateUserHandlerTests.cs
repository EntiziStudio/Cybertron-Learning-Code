using Allspark.Application.Enums;
using Allspark.Application.Exceptions;
using Allspark.Application.Mappings;
using Allspark.Application.UseCases.Users.CreateUser;
using Allspark.Application.UseCases.Users.GetUserByEmail;
using Allspark.Application.UseCases.Users.ResponseDtos;
using Allspark.Domain.Entities;

namespace Allspark.Tests.Application.UseCases.Users.CreateUser;

public class CreateUserHandlerTests
{
    private readonly Mock<ICreateUserRepository> _createUserRepositoryMock = new();
    private readonly Mock<IGetUserByEmailRepository> _getUserByEmailReponsitoryMock = new();
    private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg =>
        cfg.AddProfile<CreateUserProfile>()));

    [Fact]
    public async Task Handle_ShouldCreateUser_WhenCalledWithValidRequest()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            FullName = "Test User",
            Email = "Test Email",
            PasswordHash = Encoding.UTF8.GetBytes("This is a test passwordHash")
        };

        var expectedUser = new User
        {
            Id = 1,
            FullName = "Test Name",
            Email = "Test Email",
            Role = (int)Roles.Student,
            RefreshToken = "Test Token",
            PasswordHash = new byte[] { },
            PasswordSalt = new byte[] { },
            TokenCreatedAt = It.IsAny<DateTime>(),
            TokenExpiresAt = It.IsAny<DateTime>()
        };

        _createUserRepositoryMock.Setup(x => x.CreateUserAsync(It.IsAny<User>()))
            .ReturnsAsync(new UserResponseDto { Id = expectedUser.Id });

        var handler = new CreateUserHandler(_mapper, _getUserByEmailReponsitoryMock.Object, _createUserRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedUser.Id, result.Id);
    }

    [Fact]

    public async Task Handle_RequestWithoutEmail_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            // Name is required
            FullName = "Test User",
            PasswordHash = Encoding.UTF8.GetBytes("This is a test passwordHash")
        };

        var handler = new CreateUserHandler(_mapper, _getUserByEmailReponsitoryMock.Object, _createUserRepositoryMock.Object);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<AllsparkValidationException>(() => handler.Handle(command, CancellationToken.None));
        Assert.Contains(CreateUserConstants.EmailNotEmpty, ex.Errors);
    }

    [Fact]

    public async Task Handle_UserExisted_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            // Email is required
            FullName = "Test User",
            Email = "existed@email.com",
            PasswordHash = Encoding.UTF8.GetBytes("This is a test passwordHash")
        };

        _getUserByEmailReponsitoryMock.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(new UserResponseDto { Email = command.Email });
        
        var handler = new CreateUserHandler(_mapper, _getUserByEmailReponsitoryMock.Object, _createUserRepositoryMock.Object);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<AllsparkValidationException>(() => handler.Handle(command, CancellationToken.None));
        Assert.Contains(CreateUserConstants.UserAlreadyExists, ex.Errors);
    }
}
