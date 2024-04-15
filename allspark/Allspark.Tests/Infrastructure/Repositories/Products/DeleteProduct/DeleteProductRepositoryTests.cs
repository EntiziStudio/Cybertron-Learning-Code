using Allspark.Application.UseCases.Products.DeleteProduct;
using Allspark.Domain.Entities;
using Allspark.Infrastructure.Data;
using Allspark.Infrastructure.Mappings;
using Allspark.Infrastructure.Repositories.Products.DeleteProduct;
using Microsoft.EntityFrameworkCore;

namespace Allspark.Tests.Infrastructure.Repositories.Products.DeleteProduct;

public class DeleteProductRepositoryTests
{
    private readonly Mock<AllsparkDbContext> _dbContextMock = new(new DbContextOptionsBuilder<AllsparkDbContext>()
        .UseInMemoryDatabase(databaseName: "test")
        .Options);
    private readonly Mock<IMapper> _mapperMock = new();
    public DeleteProductRepositoryTests()
    {
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<DeleteProductRepoProfile>();
        });

        _mapperMock.Setup(mapper => mapper.ConfigurationProvider).Returns(mapperConfiguration);
    }

    [Fact]
    public async Task DeleteProductRepository_DeleteAsync_ReturnsDeletedProductResponseDto()
    {
        // Arrange
        var productId = 1;
        var product = new Product
        {
            Id = productId,
            Name = "Test Product",
            Price = 9.99m,
            Description = "This is a test product",
            CreatedDate = It.IsAny<DateTime>(),
            LastModifiedDate = It.IsAny<DateTime>(),
            Deleted = false
        };
        var deleteProductResponseDto = new DeleteProductResponseDto
        {
            Id = productId,
            Deleted = true,
        };
        var repository = new DeleteProductRepository(_dbContextMock.Object, _mapperMock.Object);

        var mockDbSet = new Mock<DbSet<Product>>();
        mockDbSet.Setup(m => m.FindAsync(productId)).ReturnsAsync(product);

        _dbContextMock.Setup(db => db.Products).Returns(mockDbSet.Object);
        _dbContextMock.Setup(db => db.SaveChangesAsync(default)).ReturnsAsync(1);
        _mapperMock.Setup(mapper => mapper.Map<Product, DeleteProductResponseDto>(It.IsAny<Product>())).Returns(deleteProductResponseDto);

        // Act
        var result = await repository.DeleteAsync(productId);

        // Assert
        Assert.Equal(deleteProductResponseDto, result);
    }

    [Fact]
    public async Task DeleteProductRepository_DeleteAsync_InvalidProductId_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var repository = new DeleteProductRepository(_dbContextMock.Object, _mapperMock.Object);
        var invalidProductId = 0;

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => repository.DeleteAsync(invalidProductId));
    }

    [Fact]
    public async Task DeleteProductRepository_DeleteAsync_ProductNotFound_ReturnsNull()
    {
        // Arrange
        var repository = new DeleteProductRepository(_dbContextMock.Object, _mapperMock.Object);
        var productId = 1;
        var mockDbSet = new Mock<DbSet<Product>>();
        mockDbSet.Setup(m => m.FindAsync(productId)).ReturnsAsync(null as Product);

        _dbContextMock.Setup(db => db.Products).Returns(mockDbSet.Object);

        // Act
        var result = await repository.DeleteAsync(productId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteProductRepository_DeleteAsync_ProductNotDeleted_ReturnsNull()
    {
        // Arrange
        var repository = new DeleteProductRepository(_dbContextMock.Object, _mapperMock.Object);
        var productId = 1;
        var product = new Product
        {
            Id = productId,
            Name = "Test Product",
            Price = 9.99m,
            Description = "This is a test product",
            CreatedDate = It.IsAny<DateTime>(),
            LastModifiedDate = It.IsAny<DateTime>(),
            Deleted = false
        };

        var mockDbSet = new Mock<DbSet<Product>>();
        mockDbSet.Setup(m => m.FindAsync(productId)).ReturnsAsync(product);

        _dbContextMock.Setup(db => db.Products).Returns(mockDbSet.Object);
        _dbContextMock.Setup(db => db.SaveChangesAsync(default)).ReturnsAsync(0);

        // Act
        var result = await repository.DeleteAsync(productId);

        // Assert
        Assert.Null(result);
    }
}

