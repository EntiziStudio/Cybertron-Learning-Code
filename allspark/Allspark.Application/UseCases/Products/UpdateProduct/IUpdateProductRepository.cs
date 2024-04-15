using Allspark.Application.UseCases.Products.ResponseDtos;

namespace Allspark.Application.UseCases.Products.UpdateProduct;

public interface IUpdateProductRepository
{
    Task<ProductResponseDto?> UpdateAsync(UpdateProductRepoParam? product);
}