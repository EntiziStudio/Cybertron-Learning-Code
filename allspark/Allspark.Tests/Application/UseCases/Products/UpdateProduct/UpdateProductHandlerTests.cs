using Allspark.Application.Mappings;
using Allspark.Application.UseCases.Products.GetProductById;
using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Application.UseCases.Products.UpdateProduct;

namespace Allspark.Tests.Application.UseCases.Products.UpdateProduct;

public class UpdateProductHandlerTests
{
    private readonly Mock<IGetProductByIdRepository> _getProductByIdRepositoryMock = new();

    private readonly Mock<IUpdateProductRepository> _updateProductRepositoryMock = new();

    private readonly IMapper _mapper;

    public UpdateProductHandlerTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UpdateProductProfile>();
        });
        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsProductResponseDto()
    {
        // Arrange
        var command = new UpdateProductCommand
        {
            Id = 1,
            Name = "Test Product",
            Price = 9.99m,
            Description = "This is a test product"
        };

        var product = new ProductResponseDto
        {
            Id = command.Id,
            Name = "Existing Product",
            Price = 19.99m,
            Description = "This is an existing product"
        };

        _getProductByIdRepositoryMock.Setup(repo => repo.GetByIdAsync(command.Id))
            .ReturnsAsync((ProductResponseDto)null!);

        _updateProductRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<UpdateProductRepoParam>()))
            .ReturnsAsync(new ProductResponseDto
            {
                Id = command.Id,
                Name = command.Name,
                Price = command.Price,
                Description = command.Description
            });

        var handler = new UpdateProductHandler(_getProductByIdRepositoryMock.Object,
            _updateProductRepositoryMock.Object, _mapper);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(command.Id, result.Id);
        Assert.Equal(command.Name, result.Name);
        Assert.Equal(command.Price, result.Price);
        Assert.Equal(command.Description, result.Description);

        _getProductByIdRepositoryMock.Verify(repo => repo.GetByIdAsync(command.Id), Times.Once);
        _updateProductRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<UpdateProductRepoParam>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidRequest_ThrowsArgumentNullException()
    {
        // Arrange
        var command = new UpdateProductCommand();

        var handler = new UpdateProductHandler(_getProductByIdRepositoryMock.Object,
            _updateProductRepositoryMock.Object, _mapper);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(command, CancellationToken.None));
    }
    [Fact]
    public async Task Handle_ProductNotFound_ReturnsNull()
    {
        // Arrange
        var command = new UpdateProductCommand
        {
            Id = 1,
            Name = "Test Product",
            Price = 9.99m,
            Description = "This is a test product"
        };

        _getProductByIdRepositoryMock.Setup(repo => repo.GetByIdAsync(command.Id))
            .ReturnsAsync((ProductResponseDto)null!);

        var handler = new UpdateProductHandler(_getProductByIdRepositoryMock.Object,
            _updateProductRepositoryMock.Object, _mapper);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Null(result);

        _getProductByIdRepositoryMock.Verify(repo => repo.GetByIdAsync(command.Id), Times.Once);
        _updateProductRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<UpdateProductRepoParam>()), Times.Never);
    }
}
