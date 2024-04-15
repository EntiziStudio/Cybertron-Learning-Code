using Allspark.Application.UseCases.Products.ResponseDtos;

namespace Allspark.Application.UseCases.Products.GetAllProducts;

public interface IGetAllProductsRepository
{
    Task<IEnumerable<ProductResponseDto>> GetAllAsync();
}