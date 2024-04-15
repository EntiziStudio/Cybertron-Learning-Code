using Allspark.Application.UseCases.Products.GetProductById;
using Allspark.Application.UseCases.Products.ResponseDtos;

namespace Allspark.Application.UseCases.Products.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductResponseDto>
{
    private readonly IGetProductByIdRepository _getProductByIdRepository;
    private readonly IUpdateProductRepository _updateProductRepository;
    private readonly IMapper _mapper;

    public UpdateProductHandler(IGetProductByIdRepository getProductByIdRepository, IUpdateProductRepository updateProductRepository, IMapper mapper)
    {
        _getProductByIdRepository = getProductByIdRepository ?? throw new ArgumentNullException(nameof(getProductByIdRepository));
        _updateProductRepository = updateProductRepository ?? throw new ArgumentNullException(nameof(updateProductRepository));
        _mapper = mapper;
    }

    public async Task<ProductResponseDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        if (request == null || request.Name == null || request.Price < 0)
        {
            throw new ArgumentNullException(request == null ? nameof(request) :
                (request.Name != null ? nameof(request.Price) : nameof(request.Name)));
        }

        // Get the product by ID
        var product = _mapper.Map<ProductResponseDto, UpdateProductRepoParam>((await _getProductByIdRepository.GetByIdAsync(request.Id))!);

        if (product == null)
        {
            return null!;
        }

        // Update the product fields
        product.Name = request.Name;
        product.Price = request.Price;
        product.Description = request.Name;
        product.LastModifiedDate = DateTime.UtcNow;

        // Update the product in the repository
        var result = await _updateProductRepository.UpdateAsync(product);

        return result!;
    }
}