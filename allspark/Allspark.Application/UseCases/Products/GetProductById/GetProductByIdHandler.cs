using Allspark.Application.UseCases.Products.ResponseDtos;

namespace Allspark.Application.UseCases.Products.GetProductById;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductResponseDto>
{
    private readonly IGetProductByIdRepository _getProductByIdRepository;

    public GetProductByIdHandler(IGetProductByIdRepository getProductByIdRepository)
    {
        _getProductByIdRepository = getProductByIdRepository ?? throw new ArgumentNullException(nameof(getProductByIdRepository));
    }

    public async Task<ProductResponseDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _getProductByIdRepository.GetByIdAsync(request.Id);

        if (product == null)
        {
            return null!;
        }

        return product;
    }
}