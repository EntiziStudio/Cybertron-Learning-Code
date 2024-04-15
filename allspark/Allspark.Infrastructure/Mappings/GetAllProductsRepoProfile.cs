using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Domain.Entities;

namespace Allspark.Infrastructure.Mappings;

public class GetAllProductsRepoProfile : Profile
{
    public GetAllProductsRepoProfile()
    {
        CreateMap<Product, ProductResponseDto>();
    }
}