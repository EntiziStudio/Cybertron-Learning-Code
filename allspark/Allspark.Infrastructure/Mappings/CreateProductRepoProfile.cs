using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Domain.Entities;

namespace Allspark.Infrastructure.Mappings;

public class CreateProductRepoProfile : Profile
{
    public CreateProductRepoProfile()
    {
        CreateMap<Product, ProductResponseDto>();
    }
}