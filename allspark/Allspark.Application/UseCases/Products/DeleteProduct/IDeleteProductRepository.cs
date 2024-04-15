namespace Allspark.Application.UseCases.Products.DeleteProduct;

public interface IDeleteProductRepository
{
    Task<DeleteProductResponseDto> DeleteAsync(int productId);
}