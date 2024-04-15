namespace Allspark.Application.UseCases.Products.DeleteProduct;

public class DeleteProductCommand : IRequest<DeleteProductResponseDto>
{
    public int ProductId { get; set; }
}