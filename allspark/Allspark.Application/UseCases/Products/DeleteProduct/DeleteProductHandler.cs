namespace Allspark.Application.UseCases.Products.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResponseDto?>
{
    private readonly IDeleteProductRepository _deleteProductRepository;

    public DeleteProductHandler(IDeleteProductRepository deleteProductRepository)
    {
        _deleteProductRepository = deleteProductRepository ?? throw new ArgumentNullException(nameof(deleteProductRepository));
    }

    public async Task<DeleteProductResponseDto?> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        return await _deleteProductRepository.DeleteAsync(request.ProductId);
    }
}