using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Application.UseCases.Products.UpdateProduct;
using Allspark.Web.Controllers.Products.UpdateProduct;

namespace Allspark.Web.Tests.Controllers.Products.UpdateProduct;

public class UpdateProductControllerTests
{
    [Fact]
    public async Task UpdateProductAsync_ValidId_ReturnsUpdatedProductResponseDto()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var controller = new UpdateProductController(mediatorMock.Object);
        var productId = 1;
        var command = new UpdateProductCommand { Id = productId, Name = "Updated Product" };
        var updatedProduct = new ProductResponseDto { Id = productId, Name = command.Name };

        mediatorMock.Setup(mediator => mediator.Send(
                It.Is<UpdateProductCommand>(req => req.Id == productId && req.Name == "Updated Product"),
                It.IsAny<CancellationToken>()))
        .ReturnsAsync(updatedProduct);

        // Act
        var result = await controller.UpdateProductAsync(productId, command);

        // Assert
        var returnedProduct = Assert.IsType<ProductResponseDto>(result.Value);
        Assert.Equal(updatedProduct.Id, returnedProduct.Id);
        Assert.Equal(updatedProduct.Name, returnedProduct.Name);
    }

    [Fact]
    public async Task UpdateProductAsync_InvalidId_ReturnsBadRequest()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var controller = new UpdateProductController(mediatorMock.Object);
        var productId = 1;
        var command = new UpdateProductCommand { Id = 2, Name = "Updated Product" };

        // Act
        var result = await controller.UpdateProductAsync(productId, command);

        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task UpdateProductAsync_ProductNotFound_ReturnsNotFound()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var controller = new UpdateProductController(mediatorMock.Object);
        var productId = 1;
        var command = new UpdateProductCommand { Id = productId, Name = "Updated Product" };
        ProductResponseDto product = null;

        mediatorMock.Setup(mediator => mediator.Send(
                It.Is<UpdateProductCommand>(req => req.Id == productId && req.Name == "Updated Product"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        // Act
        var result = await controller.UpdateProductAsync(productId, command);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}
