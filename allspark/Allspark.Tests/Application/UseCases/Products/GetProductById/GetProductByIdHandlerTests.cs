using Allspark.Application.UseCases.Products.GetProductById;
using Allspark.Application.UseCases.Products.ResponseDtos;

namespace Allspark.Tests.Application.UseCases.Products.GetProductById;

public class GetProductByIdHandlerTests
{
    [Fact]
    public async Task Handle_ProductExists_ShouldReturnProductResponse()
    {
        // Arrange
        int productId = 1;
        var getProductByIdQuery = new GetProductByIdQuery { Id = productId };
        var expectedResponse = new ProductResponseDto { Id = productId, Name = "Product 1", Price = 9.99m };
        var mockGetProductByIdRepository = new Mock<IGetProductByIdRepository>();
        mockGetProductByIdRepository.Setup(repo => repo.GetByIdAsync(productId))
            .ReturnsAsync(expectedResponse);
        var handler = new GetProductByIdHandler(mockGetProductByIdRepository.Object);

        // Act
        var actualResponse = await handler.Handle(getProductByIdQuery, CancellationToken.None);

        // Assert
        Assert.Equal(expectedResponse, actualResponse);
    }

    [Fact]
    public async Task Handle_ProductDoesNotExist_ShouldReturnNotFoundResponse()
    {
        // Arrange
        int productId = 1;
        var getProductByIdQuery = new GetProductByIdQuery { Id = productId };
        ProductResponseDto? expectedResponse = null;
        var mockGetProductByIdRepository = new Mock<IGetProductByIdRepository>();
        mockGetProductByIdRepository.Setup(repo => repo.GetByIdAsync(productId))
            .ReturnsAsync((ProductResponseDto)null);
        var handler = new GetProductByIdHandler(mockGetProductByIdRepository.Object);

        // Act
        var actualResponse = await handler.Handle(getProductByIdQuery, CancellationToken.None);

        // Assert
        Assert.Equal(expectedResponse, actualResponse);
    }
}
