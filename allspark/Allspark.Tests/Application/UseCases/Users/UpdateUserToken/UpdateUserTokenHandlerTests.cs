using Allspark.Application.UseCases.Users.GetUserById;
using Allspark.Application.UseCases.Users.ResponseDtos;
using Allspark.Application.UseCases.Users.UpdateUserToken;
using Allspark.Application.Mappings;
using Allspark.Application.Exceptions;

namespace Allspark.Tests.Application.UseCases.Users.UpdateUserToken;

public class UpdateUserTokenHandlerTests
{
    private readonly Mock<IUpdateUserTokenRepository> _updateUserTokenRepositoryMock = new();
    private readonly Mock<IGetUserByIdRepository> _getUserByIdRepositoryMock = new();
    private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg =>
        cfg.AddProfile<UpdateUserTokenProfile>()));

    [Fact]
    public async Task Handle_ShouldUpdateUserToken_WhenCalledWithValidRequest()
    {
        // Arrange
        var command = new UpdateUserTokenCommand
        {
            UserId = 1,
            RefreshToken = "updated_token"
        };

        var user = new UserResponseDto
        {
            Id = 1,
            RefreshToken = "updated_token"
        };

        _getUserByIdRepositoryMock.Setup(repo => repo.GetUserByIdAsync(command.UserId))
   .ReturnsAsync(user);

        _updateUserTokenRepositoryMock.Setup(repo => repo.UpdateUserTokenAsync(It.IsAny<UpdateUserTokenRepoParam>()))
    .ReturnsAsync(new UserResponseDto
    {
        Id = command.UserId,
        RefreshToken = command.RefreshToken,

    });

        var handler = new UpdateUserTokenHandler(_mapper, _getUserByIdRepositoryMock.Object, _updateUserTokenRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(command.UserId, result.Id);
        Assert.Equal(command.RefreshToken, result.RefreshToken);

        _getUserByIdRepositoryMock.Verify(repo => repo.GetUserByIdAsync(command.UserId), Times.Once);
        _updateUserTokenRepositoryMock.Verify(repo => repo.UpdateUserTokenAsync(It.IsAny<UpdateUserTokenRepoParam>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_UserNotFound_ThrowsValidationException()
    {
        // Arrange
        var command = new UpdateUserTokenCommand
        {
            UserId = 1,
            RefreshToken = "update_token",
        };

        var handler = new UpdateUserTokenHandler(_mapper, _getUserByIdRepositoryMock.Object,
            _updateUserTokenRepositoryMock.Object);
        // Assert
        await Assert.ThrowsAsync<AllsparkValidationException>(() => handler.Handle(command, CancellationToken.None));
    }
}