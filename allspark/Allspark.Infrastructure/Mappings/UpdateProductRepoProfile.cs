using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Domain.Entities;

namespace Allspark.Infrastructure.Mappings;

public class UpdateProductRepoProfile : Profile
{
    public UpdateProductRepoProfile()
    {
        CreateMap<Product, ProductResponseDto>();
    }
}