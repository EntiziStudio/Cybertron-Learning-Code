using Allspark.Application.UseCases.Products.DeleteProduct;
using Allspark.Domain.Entities;

namespace Allspark.Infrastructure.Mappings;

public class DeleteProductRepoProfile : Profile
{
    public DeleteProductRepoProfile()
    {
        CreateMap<Product, DeleteProductResponseDto>();
    }
}