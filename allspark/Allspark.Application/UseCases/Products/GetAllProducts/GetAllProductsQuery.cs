using Allspark.Application.UseCases.Products.ResponseDtos;

namespace Allspark.Application.UseCases.Products.GetAllProducts;

public class GetAllProductsQuery : IRequest<IEnumerable<ProductResponseDto>>
{
}