using Allspark.Application.UseCases.Products.GetAllProducts;
using Allspark.Application.UseCases.Products.ResponseDtos;

namespace Allspark.Tests.Application.UseCases.Products.GetAllProducts;
public class GetAllProductsHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllProducts_WhenCalled()
    {
        // Arrange
        var expectedProducts = new List<ProductResponseDto>
            {
                new ProductResponseDto { Id = 1, Name = "Product 1", Price = 9.99m },
                new ProductResponseDto { Id = 2, Name = "Product 2", Price = 19.99m },
                new ProductResponseDto { Id = 3, Name = "Product 3", Price = 29.99m }
            };

        var mockRepository = new Mock<IGetAllProductsRepository>();
        mockRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(expectedProducts);

        var handler = new GetAllProductsHandler(mockRepository.Object);

        // Act
        var actualProducts = await handler.Handle(new GetAllProductsQuery(), CancellationToken.None);

        // Assert
        Assert.Equal(expectedProducts, actualProducts);
    }
    [Fact]
    public async Task Handle_NoProducts_ReturnsEmptyList()
    {
        // Arrange
        var mockRepository = new Mock<IGetAllProductsRepository>();
        mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<ProductResponseDto>());

        var handler = new GetAllProductsHandler(mockRepository.Object);
        var query = new GetAllProductsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Empty(result);
    }
    [Fact]
    public async Task Handle_RepositoryWithProducts_ReturnsCorrectNumberOfProducts()
    {
        // Arrange
        var productList = new List<ProductResponseDto>
            {
                new ProductResponseDto { Id = 1, Name = "Product 1" },
                new ProductResponseDto { Id = 2, Name = "Product 2" },
                new ProductResponseDto { Id = 3, Name = "Product 3" }
            };

        var mockRepository = new Mock<IGetAllProductsRepository>();
        mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(productList);

        var handler = new GetAllProductsHandler(mockRepository.Object);
        var query = new GetAllProductsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(productList.Count, result.Count());
    }
    [Fact]
    public async Task Handle_RepositoryThrowsException_ThrowsException()
    {
        // Arrange
        var mockRepository = new Mock<IGetAllProductsRepository>();
        mockRepository.Setup(r => r.GetAllAsync()).ThrowsAsync(new Exception("Repository exception"));

        var handler = new GetAllProductsHandler(mockRepository.Object);
        var query = new GetAllProductsQuery();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(query, CancellationToken.None));
    }

}
