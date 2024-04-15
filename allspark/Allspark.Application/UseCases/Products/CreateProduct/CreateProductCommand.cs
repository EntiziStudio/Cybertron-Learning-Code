using Allspark.Application.UseCases.Products.ResponseDtos;

namespace Allspark.Application.UseCases.Products.CreateProduct;

public class CreateProductCommand : IRequest<ProductResponseDto>
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }

    public bool HasAnyEmptyProperty()
    {
        var type = GetType();
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        return properties.Select(x => x.GetValue(this, null))
            .Any(x => (x == null) || (String.IsNullOrWhiteSpace(x.ToString())));
    }
}