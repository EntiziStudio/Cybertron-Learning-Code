using Allspark.Application.UseCases.Products.ResponseDtos;

namespace Allspark.Application.UseCases.Products.GetProductById;

public class GetProductByIdQuery : IRequest<ProductResponseDto>
{
    public int Id { get; set; }
}