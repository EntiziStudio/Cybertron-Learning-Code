using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Application.UseCases.Products.UpdateProduct;
using Allspark.Domain.Entities;
using Allspark.Infrastructure.Data;
using Allspark.Infrastructure.Mappings;
using Allspark.Infrastructure.Repositories.Products.UpdateProduct;
using Microsoft.EntityFrameworkCore;

namespace Allspark.Tests.Infrastructure.Repositories.Products.UpdateProduct;

public class UpdateProductRepositoryTests
{
    [Fact]
    public async Task UpdateAsync_ReturnsProductResponseDto_WhenProductExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AllsparkDbContext>()
            .UseInMemoryDatabase(databaseName: "UpdateAsync_ReturnsProductResponseDto_WhenProductExists")
            .Options;
        var dbContext = new AllsparkDbContext(options);
        var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<UpdateProductRepoProfile>()));
        var repository = new UpdateProductRepository(dbContext, mapper);

        var product = new Product { Name = "Product 1", Price = 10.99m, Description = "Description 1", Deleted = false };
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();

        var updatedProduct = new UpdateProductRepoParam { Id = product.Id, Name = "Product 2", Price = 15.99m, Description = "Description 2" };

        // Act
        var result = await repository.UpdateAsync(updatedProduct);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProductResponseDto>(result);
        Assert.Equal(updatedProduct.Name, result!.Name);
        Assert.Equal(updatedProduct.Price, result.Price);
        Assert.Equal(updatedProduct.Description, result.Description);
    }
    
    [Fact]
    public async Task UpdateAsync_ThrowsArgumentNullException_WhenProductIsNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AllsparkDbContext>()
            .UseInMemoryDatabase(databaseName: "UpdateAsync_ThrowsArgumentNullException_WhenProductIsNull")
            .Options;
        var dbContext = new AllsparkDbContext(options);
        var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<UpdateProductRepoProfile>()));
        var repository = new UpdateProductRepository(dbContext, mapper);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => repository.UpdateAsync(null));
    }

    [Fact]
    public async Task UpdateAsync_ThrowsInvalidOperationException_WhenProductNotFound()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AllsparkDbContext>()
            .UseInMemoryDatabase(databaseName: "UpdateAsync_ThrowsInvalidOperationException_WhenProductNotFound")
            .Options;
        var dbContext = new AllsparkDbContext(options);
        var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<UpdateProductRepoProfile>()));
        var repository = new UpdateProductRepository(dbContext, mapper);

        // Act and Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            repository.UpdateAsync(new UpdateProductRepoParam
            {
                Id = 1,
                Name = "Product 1",
                Price = 10.99m,
                Description = "Description 1",
            })
        );
    }

}