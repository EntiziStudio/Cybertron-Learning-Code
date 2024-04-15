using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Domain.Entities;

namespace Allspark.Application.UseCases.Products.CreateProduct;

public interface ICreateProductRepository
{
    Task<ProductResponseDto> CreateAsync(Product product);
}