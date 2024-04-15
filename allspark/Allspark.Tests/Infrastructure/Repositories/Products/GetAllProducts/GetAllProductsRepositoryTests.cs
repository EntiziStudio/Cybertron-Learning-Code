using Allspark.Domain.Entities;
using Allspark.Infrastructure.Data;
using Allspark.Infrastructure.Mappings;
using Allspark.Infrastructure.Repositories.Products.GetAllProducts;
using Microsoft.EntityFrameworkCore;

namespace Allspark.Tests.Infrastructure.Repositories.Products.GetAllProducts;

public class GetAllProductsRepositoryTests
{
    private readonly IMapper _mapper;
    private readonly AllsparkDbContext _dbContext;

    public GetAllProductsRepositoryTests()
    {
        _dbContext = new AllsparkDbContext(new DbContextOptionsBuilder<AllsparkDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<GetAllProductsRepoProfile>();
        }).CreateMapper();

        // Seed the in-memory database with test data
        var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Deleted = false },
                new Product { Id = 2, Name = "Product 2", Deleted = false },
                new Product { Id = 3, Name = "Product 3", Deleted = true }
            };
        _dbContext.Products.AddRange(products);
        _dbContext.SaveChanges();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOnlyNonDeletedProducts()
    {
        // Arrange
        var repository = new GetAllProductsRepository(_dbContext, _mapper);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.DoesNotContain(result, p => p.Name == "Product 3");
    }

    [Fact]
    public async Task GetAllAsync_ReturnsEmptyList_WhenNoProductsExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AllsparkDbContext>()
            .UseInMemoryDatabase(databaseName: "GetAllAsync_ReturnsEmptyList_WhenNoProductsExist")
            .Options;
        var dbContext = new AllsparkDbContext(options);
        var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<GetAllProductsRepoProfile>()));
        var repository = new GetAllProductsRepository(dbContext, mapper);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsNonDeletedProducts()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AllsparkDbContext>()
            .UseInMemoryDatabase(databaseName: "GetAllAsync_ReturnsNonDeletedProducts")
            .Options;
        var dbContext = new AllsparkDbContext(options);
        var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<GetAllProductsRepoProfile>()));
        var repository = new GetAllProductsRepository(dbContext, mapper);

        // Add some products to the database
        dbContext.Products.Add(new Product { Name = "Product 1", Deleted = true });
        dbContext.Products.Add(new Product { Name = "Product 2", Deleted = false });
        dbContext.Products.Add(new Product { Name = "Product 3", Deleted = false });
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, p => p.Name == "Product 2");
        Assert.Contains(result, p => p.Name == "Product 3");
    }

    [Fact]
    public async Task GetAllAsync_ReturnsMappedProducts()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AllsparkDbContext>()
            .UseInMemoryDatabase(databaseName: "GetAllAsync_ReturnsMappedProducts")
            .Options;
        var dbContext = new AllsparkDbContext(options);
        var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<GetAllProductsRepoProfile>()));
        var repository = new GetAllProductsRepository(dbContext, mapper);

        // Add some products to the database
        dbContext.Products.Add(new Product { Name = "Product 1", Price = 10.99m, Description = "Description 1", Deleted = false });
        dbContext.Products.Add(new Product { Name = "Product 2", Price = 20.99m, Description = "Description 2", Deleted = false });
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, p => p.Name == "Product 1" && p.Price == 10.99m && p.Description == "Description 1");
        Assert.Contains(result, p => p.Name == "Product 2" && p.Price == 20.99m && p.Description == "Description 2");
    }

}

