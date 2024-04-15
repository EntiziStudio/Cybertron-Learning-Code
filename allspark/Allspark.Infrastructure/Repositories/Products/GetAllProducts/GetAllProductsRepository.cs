using Allspark.Application.UseCases.Products.GetAllProducts;
using Allspark.Domain.Entities;
using Allspark.Infrastructure.Data;
using Allspark.Application.UseCases.Products.ResponseDtos;

namespace Allspark.Infrastructure.Repositories.Products.GetAllProducts;

public class GetAllProductsRepository : IGetAllProductsRepository
{
    private readonly AllsparkDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllProductsRepository(AllsparkDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
    {
        return _mapper.Map<List<Product>, IEnumerable<ProductResponseDto>>(await _dbContext.Products.Where(c => !c.Deleted.Equals(true)).ToListAsync());
    }
}