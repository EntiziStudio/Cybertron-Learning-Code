using Allspark.Application.UseCases.Products.GetProductById;
using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Domain.Entities;
using Allspark.Infrastructure.Data;

namespace Allspark.Infrastructure.Repositories.Products.GetProductById;

public class GetProductByIdRepository : IGetProductByIdRepository
{
    private readonly AllsparkDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetProductByIdRepository(AllsparkDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper;
    }

    public async Task<ProductResponseDto?> GetByIdAsync(int productId)
    {
        return _mapper.Map<Product, ProductResponseDto>((await _dbContext.Set<Product>().FindAsync(productId))!);
    }
}