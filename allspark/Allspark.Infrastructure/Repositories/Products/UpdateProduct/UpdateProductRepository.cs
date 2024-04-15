using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Application.UseCases.Products.UpdateProduct;
using Allspark.Domain.Entities;
using Allspark.Infrastructure.Data;

namespace Allspark.Infrastructure.Repositories.Products.UpdateProduct;

public class UpdateProductRepository : IUpdateProductRepository
{
    private readonly AllsparkDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateProductRepository(AllsparkDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper;
    }

    public async Task<ProductResponseDto?> UpdateAsync(UpdateProductRepoParam? product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == product.Id);

        if (existingProduct != null)
        {
            _dbContext.Entry(existingProduct).CurrentValues.SetValues(product);
            int affectedRows = await _dbContext.SaveChangesAsync();
            if (affectedRows > 0)
            {
                return _mapper.Map<Product, ProductResponseDto>(existingProduct);
            }
        }

        throw new InvalidOperationException($"Product with id {product.Id} not found in UpdateProductRepository");
    }
}