using Allspark.Application.UseCases.Products.DeleteProduct;
using Allspark.Domain.Entities;
using Allspark.Infrastructure.Data;

namespace Allspark.Infrastructure.Repositories.Products.DeleteProduct;

public class DeleteProductRepository : IDeleteProductRepository
{
    private readonly AllsparkDbContext _dbContext;
    private readonly IMapper _mapper;

    public DeleteProductRepository(AllsparkDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper;
    }

    public async Task<DeleteProductResponseDto> DeleteAsync(int productId)
    {
        if (productId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(productId), "Product ID must be greater than zero.");
        }

        var product = await _dbContext.Products.FindAsync(productId);
        if (product == null)
        {
            return null!;
        }

        product.Deleted = true;
        int affectedRows = await _dbContext.SaveChangesAsync();
        if (affectedRows > 0)
        {
            return _mapper.Map<Product, DeleteProductResponseDto>(product);
        }

        return null!;
    }
}