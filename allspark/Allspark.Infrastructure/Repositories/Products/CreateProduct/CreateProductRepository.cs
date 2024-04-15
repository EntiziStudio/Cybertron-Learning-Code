using Allspark.Application.UseCases.Products.CreateProduct;
using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Domain.Entities;
using Allspark.Infrastructure.Data;

namespace Allspark.Infrastructure.Repositories.Products.CreateProduct;

public class CreateProductRepository : ICreateProductRepository
{
    private readonly AllsparkDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateProductRepository(AllsparkDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper;
    }

    public async Task<ProductResponseDto> CreateAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        await _dbContext.Set<Product>().AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<Product, ProductResponseDto>(product);
    }
}