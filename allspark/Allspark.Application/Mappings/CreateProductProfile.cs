using Allspark.Application.UseCases.Products.CreateProduct;
using Allspark.Domain.Entities;

namespace Allspark.Application.Mappings;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductCommand, Product>();
    }
}