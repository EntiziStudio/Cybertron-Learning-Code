using Allspark.Application.Mappings;
using Allspark.Application.UseCases.Products.CreateProduct;
using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Domain.Entities;

namespace Allspark.Tests.Application.UseCases.Products.CreateProduct;
public class CreateProductHandlerTests
{
    private readonly Mock<ICreateProductRepository> _createProductRepositoryMock = new();
    
    private readonly IMapper _mapper;

    public CreateProductHandlerTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CreateProductProfile>();
        });
        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public async Task Handle_ShouldCreateProduct_WhenCalledWithValidRequest()
    {
        // Arrange
        var command = new CreateProductCommand
        {
            Name = "Test Product",
            Price = 9.99m,
            Description = "This is a test product"
        };

        var expectedProduct = new Product
        {
            Id = 1,
            Name = "Test Product",
            Price = 9.99m,
            Description = "This is a test product",
            CreatedDate = It.IsAny<DateTime>(),
            LastModifiedDate = It.IsAny<DateTime>(),
            Deleted = false
        };

        _createProductRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Product>()))
            .ReturnsAsync(new ProductResponseDto { Id = expectedProduct.Id });

        var handler = new CreateProductHandler(_createProductRepositoryMock.Object, _mapper);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedProduct.Id, result.Id);
    }

    [Fact]
    public async Task Handle_ShouldNotCreateProduct_WhenCalledWithInvalidRequest()
    {
        // Arrange
        var command = new CreateProductCommand
        {
            // Name is required
            Price = 9.99m,
            Description = "This is a test product"
        };

        var handler = new CreateProductHandler(_createProductRepositoryMock.Object, _mapper);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(command, CancellationToken.None));
    }
}
