using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Domain.Entities;
using Allspark.Infrastructure.Data;
using Allspark.Infrastructure.Mappings;
using Allspark.Infrastructure.Repositories.Products.CreateProduct;
using Microsoft.EntityFrameworkCore;

namespace Allspark.Tests.Infrastructure.Repositories.Products.CreateProduct;

public class CreateProductRepositoryTests
{
    private readonly Mock<AllsparkDbContext> _dbContextMock = new(new DbContextOptions<AllsparkDbContext>());
    private readonly Mock<IMapper> _mapperMock = new();
    public CreateProductRepositoryTests()
    {
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CreateProductRepoProfile>();
        });

        _mapperMock.Setup(mapper => mapper.ConfigurationProvider).Returns(mapperConfiguration);
    }

    [Fact]
    public async Task CreateProductRepository_CreateAsync_ReturnsCorrectProductResponseDto()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Test Product",
            Price = 9.99m,
            Description = "This is a test product",
            CreatedDate = It.IsAny<DateTime>(),
            LastModifiedDate = It.IsAny<DateTime>(),
            Deleted = false
        };
        var productResponseDto = new ProductResponseDto
        {
            Id = 1,
            Name = "Test Product",
            Price = 9.99m,
            Description = "This is a test product",
            CreatedDate = It.IsAny<DateTime>(),
            LastModifiedDate = It.IsAny<DateTime>(),
        };
        var repository = new CreateProductRepository(_dbContextMock.Object, _mapperMock.Object);
        _dbContextMock.Setup(db => db.Set<Product>().AddAsync(product, default)).Verifiable();
        _dbContextMock.Setup(db => db.SaveChangesAsync(default)).ReturnsAsync(1);
        _mapperMock.Setup(mapper => mapper.Map<Product, ProductResponseDto>(It.IsAny<Product>())).Returns(productResponseDto);

        // Act
        var result = await repository.CreateAsync(product);

        // Assert
        Assert.Equal(productResponseDto, result);
    }

    [Fact]
    public async Task CreateProductRepository_CreateAsync_NullProduct_ThrowsArgumentNullException()
    {
        // Arrange
        var repository = new CreateProductRepository(_dbContextMock.Object, _mapperMock.Object);

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => repository.CreateAsync(null));
    }

    [Fact]
    public async Task CreateProductRepository_CreateAsync_AddsProductToDbContext()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Test Product",
            Price = 9.99m,
            Description = "This is a test product",
            CreatedDate = It.IsAny<DateTime>(),
            LastModifiedDate = It.IsAny<DateTime>(),
            Deleted = false
        };
        var repository = new CreateProductRepository(_dbContextMock.Object, _mapperMock.Object);
        _dbContextMock.Setup(db => db.Set<Product>().AddAsync(product, default)).Verifiable();

        // Act
        await repository.CreateAsync(product);

        // Assert
        _dbContextMock.Verify(db => db.Set<Product>().AddAsync(product, default), Times.Once);
    }

    [Fact]
    public async Task CreateProductRepository_CreateAsync_SavesChangesToDbContext()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Test Product",
            Price = 9.99m,
            Description = "This is a test product",
            CreatedDate = It.IsAny<DateTime>(),
            LastModifiedDate = It.IsAny<DateTime>(),
            Deleted = false
        };
        var repository = new CreateProductRepository(_dbContextMock.Object, _mapperMock.Object);
        _dbContextMock.Setup(db => db.Set<Product>().AddAsync(product, default)).Verifiable();
        _dbContextMock.Setup(db => db.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        await repository.CreateAsync(product);

        // Assert
        _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
    }

}