using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Domain.Entities;

namespace Allspark.Application.UseCases.Products.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponseDto>
{
    private readonly ICreateProductRepository _createProductRepository;
    private readonly IMapper _mapper;

    public CreateProductHandler(ICreateProductRepository createProductRepository, IMapper mapper)
    {
        _createProductRepository = createProductRepository ?? throw new ArgumentNullException(nameof(createProductRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ProductResponseDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (request.HasAnyEmptyProperty())
        {
            throw new ArgumentNullException(nameof(request));
        }

        var product = _mapper.Map<Product>(request);
        product.CreatedDate = DateTime.Now;
        product.LastModifiedDate = DateTime.Now;
        product.Deleted = false;

        return await _createProductRepository.CreateAsync(product);
    }
}