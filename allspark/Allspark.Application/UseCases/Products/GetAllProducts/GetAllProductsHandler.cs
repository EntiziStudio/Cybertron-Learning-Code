using Allspark.Application.UseCases.Products.ResponseDtos;

namespace Allspark.Application.UseCases.Products.GetAllProducts;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponseDto>>
{
    private readonly IGetAllProductsRepository _getAllProductsRepository;

    public GetAllProductsHandler(IGetAllProductsRepository getAllProductsRepository)

    {
        _getAllProductsRepository = getAllProductsRepository ?? throw new ArgumentNullException(nameof(getAllProductsRepository));
    }

    public async Task<IEnumerable<ProductResponseDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _getAllProductsRepository.GetAllAsync();
        return products;
    }
}
