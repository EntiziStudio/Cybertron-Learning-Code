using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Domain.Entities;

namespace Allspark.Infrastructure.Mappings;

public class GetProductByIdRepoProfile : Profile
{
    public GetProductByIdRepoProfile()
    {
        CreateMap<Product, ProductResponseDto>();
    }
}