using Allspark.Application.UseCases.Products.DeleteProduct;

namespace Allspark.Tests.Application.UseCases.Products.DeleteProduct;

public class DeleteProductHandlerTests
{
    [Fact]
    public async Task Handle_ProductExists_ShouldReturnSuccessResponse()
    {
        // Arrange
        int productId = 1;
        var deleteProductCommand = new DeleteProductCommand { ProductId = productId };
        var expectedResponse = new DeleteProductResponseDto { Deleted = true };
        var mockDeleteProductRepository = new Mock<IDeleteProductRepository>();
        mockDeleteProductRepository.Setup(repo => repo.DeleteAsync(productId))
            .ReturnsAsync(expectedResponse);
        var handler = new DeleteProductHandler(mockDeleteProductRepository.Object);

        // Act
        var actualResponse = await handler.Handle(deleteProductCommand, CancellationToken.None);

        // Assert
        Assert.Equal(expectedResponse, actualResponse);
    }

    [Fact]
    public async Task Handle_ProductDoesNotExist_ShouldReturnNotFoundResponse()
    {
        // Arrange
        int productId = 1;
        var deleteProductCommand = new DeleteProductCommand { ProductId = productId };
        DeleteProductResponseDto? expectedResponse = null;
        var mockDeleteProductRepository = new Mock<IDeleteProductRepository>();
        mockDeleteProductRepository.Setup(repo => repo.DeleteAsync(productId))
            .ReturnsAsync((DeleteProductResponseDto)null);
        var handler = new DeleteProductHandler(mockDeleteProductRepository.Object);

        // Act
        var actualResponse = await handler.Handle(deleteProductCommand, CancellationToken.None);

        // Assert
        Assert.Equal(expectedResponse, actualResponse);
    }
}
