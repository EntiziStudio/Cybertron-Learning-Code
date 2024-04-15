using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Application.UseCases.Products.UpdateProduct;

namespace Allspark.Application.Mappings;

public class UpdateProductProfile : Profile
{
    public UpdateProductProfile()
    {
        CreateMap<ProductResponseDto, UpdateProductRepoParam>();
    }
}
