using Allspark.Application.UseCases.Products.GetProductById;
using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Web.Controllers.Products.GetProductById;

namespace Allspark.Web.Tests.Controllers.Products.GetProductById;

public class GetProductByIdControllerTests
{
    [Fact]
    public async Task GetProductByIdAsync_ValidId_ReturnsProductResponseDto()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var controller = new GetProductByIdController(mediatorMock.Object);
        var productId = 1;
        var product = new ProductResponseDto { Id = productId, Name = "Product 1" };

        mediatorMock.Setup(mediator => mediator.Send(It.IsAny<GetProductByIdQuery>(), default))
            .ReturnsAsync(product);

        // Act
        var result = await controller.GetProductByIdAsync(productId);

        // Assert
        var response = Assert.IsType<OkObjectResult>(result.Result);
        var returnedProduct = Assert.IsType<ProductResponseDto>(response.Value);
        Assert.Equal(product.Id, returnedProduct.Id);
        Assert.Equal(product.Name, returnedProduct.Name);
    }

    [Fact]
    public async Task GetProductByIdAsync_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var controller = new GetProductByIdController(mediatorMock.Object);
        var productId = 1;
        ProductResponseDto product = null;

        mediatorMock.Setup(mediator => mediator.Send(It.IsAny<GetProductByIdQuery>(), default))
            .ReturnsAsync(product);

        // Act
        var result = await controller.GetProductByIdAsync(productId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}
