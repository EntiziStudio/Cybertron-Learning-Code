using Allspark.Application.UseCases.Products.CreateProduct;
using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Web.Controllers.Products.CreateProduct;

namespace Allspark.Web.Tests.Controllers.Products.CreateProduct;

public class CreateProductControllerTests
{
    [Fact]
    public async Task CreateProductAsync_ValidCommand_ReturnsProductResponseDto()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var controller = new CreateProductController(mediatorMock.Object);
        var command = new CreateProductCommand
        {
            Name = "Test Product",
            Price = 9.99m,
            Description = "This is a test product"
        };

        var createdProduct = new ProductResponseDto
        {
            Id = 1,
            Name = command.Name,
            Price = command.Price,
            Description = command.Description
        };

        mediatorMock.Setup(mediator => mediator.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdProduct);

        // Act
        var result = await controller.CreateProductAsync(command);

        // Assert
        var returnedProduct = Assert.IsType<ProductResponseDto>(result.Value);
        Assert.Equal(createdProduct.Id, returnedProduct.Id);
        Assert.Equal(createdProduct.Name, returnedProduct.Name);
        Assert.Equal(createdProduct.Price, returnedProduct.Price);
        Assert.Equal(createdProduct.Description, returnedProduct.Description);
    }
}
