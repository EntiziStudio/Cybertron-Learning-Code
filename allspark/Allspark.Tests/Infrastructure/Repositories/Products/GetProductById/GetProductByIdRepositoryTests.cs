using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Domain.Entities;
using Allspark.Infrastructure.Data;
using Allspark.Infrastructure.Mappings;
using Allspark.Infrastructure.Repositories.Products.GetProductById;
using Microsoft.EntityFrameworkCore;

namespace Allspark.Tests.Infrastructure.Repositories.Products.GetProductById;

public class GetProductByIdRepositoryTests
{
    [Fact]
    public async Task GetByIdAsync_ReturnsProductResponseDto_WhenProductExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AllsparkDbContext>()
            .UseInMemoryDatabase(databaseName: "GetByIdAsync_ReturnsProductResponseDto_WhenProductExists")
            .Options;
        var dbContext = new AllsparkDbContext(options);
        var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<GetProductByIdRepoProfile>()));
        var repository = new GetProductByIdRepository(dbContext, mapper);

        var product = new Product
        {
            Name = "Product 1",
            Price = 10.99m,
            Description = "Description 1",
            Deleted = false
        };
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(product.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProductResponseDto>(result);
        Assert.Equal(product.Name, result!.Name);
        Assert.Equal(product.Price, result.Price);
        Assert.Equal(product.Description, result.Description);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenProductDoesNotExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AllsparkDbContext>()
            .UseInMemoryDatabase(databaseName: "GetByIdAsync_ReturnsNull_WhenProductDoesNotExist")
            .Options;
        var dbContext = new AllsparkDbContext(options);
        var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<GetProductByIdRepoProfile>()));
        var repository = new GetProductByIdRepository(dbContext, mapper);

        // Act
        var result = await repository.GetByIdAsync(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsProductResponseDto_WhenProductIsDeleted()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AllsparkDbContext>()
            .UseInMemoryDatabase(databaseName: "GetByIdAsync_ReturnsNull_WhenProductIsDeleted")
            .Options;
        var dbContext = new AllsparkDbContext(options);
        var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<GetProductByIdRepoProfile>()));
        var repository = new GetProductByIdRepository(dbContext, mapper);

        var product = new Product
        {
            Name = "Product 1",
            Price = 10.99m,
            Description = "Description 1",
            Deleted = true
        };
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(product.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProductResponseDto>(result);
        Assert.Equal(product.Name, result!.Name);
        Assert.Equal(product.Price, result.Price);
        Assert.Equal(product.Description, result.Description);
    }
    [Fact]
    public async Task GetByIdAsync_ReturnsProductResponseDtoWithNullDescription_WhenProductDescriptionIsNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AllsparkDbContext>()
            .UseInMemoryDatabase(databaseName: "GetByIdAsync_ReturnsProductResponseDtoWithNullDescription_WhenProductDescriptionIsNull")
            .Options;
        var dbContext = new AllsparkDbContext(options);
        var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<GetProductByIdRepoProfile>()));
        var repository = new GetProductByIdRepository(dbContext, mapper);

        var product = new Product
        {
            Name = "Product 1",
            Price = 10.99m,
            Description = null,
            Deleted = false
        };
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(product.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProductResponseDto>(result);
        Assert.Equal(product.Name, result!.Name);
        Assert.Equal(product.Price, result.Price);
        Assert.Null(result.Description);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenProductIdIsZero()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AllsparkDbContext>()
            .UseInMemoryDatabase(databaseName: "GetByIdAsync_ReturnsNull_WhenProductIdIsZero")
            .Options;
        var dbContext = new AllsparkDbContext(options);
        var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<GetProductByIdRepoProfile>()));
        var repository = new GetProductByIdRepository(dbContext, mapper);

        // Act
        var result = await repository.GetByIdAsync(0);

        // Assert
        Assert.Null(result);
    }

}