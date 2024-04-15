using Allspark.Application.UseCases.Products.ResponseDtos;

namespace Allspark.Application.UseCases.Products.GetProductById;

public interface IGetProductByIdRepository
{
    Task<ProductResponseDto?> GetByIdAsync(int productId);
}