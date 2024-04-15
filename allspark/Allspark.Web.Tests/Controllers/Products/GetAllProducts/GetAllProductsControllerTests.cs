using Allspark.Application.UseCases.Products.GetAllProducts;
using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Web.Controllers.Products.GetAllProducts;

namespace Allspark.Web.Tests.Controllers.Products.GetAllProducts;

public class GetAllProductsControllerTests
{
    [Fact]
    public async Task GetAllProductsAsync_ReturnsListOfProductResponseDto()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var controller = new GetAllProductsController(mediatorMock.Object);
        var products = new List<ProductResponseDto>
            {
                new ProductResponseDto { Id = 1, Name = "Product 1" },
                new ProductResponseDto { Id = 2, Name = "Product 2" }
            };

        mediatorMock.Setup(mediator => mediator.Send(It.IsAny<GetAllProductsQuery>(), default))
            .ReturnsAsync(products);

        // Act
        var result = await controller.GetAllProductsAsync();

        // Assert
        var response = Assert.IsType<OkObjectResult>(result.Result);
        var returnedProducts = Assert.IsType<List<ProductResponseDto>>(response.Value);
        Assert.Equal(products.Count, returnedProducts.Count);
    }
}
