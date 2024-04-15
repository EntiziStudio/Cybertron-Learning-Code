using Allspark.Application.UseCases.Products.DeleteProduct;

namespace Allspark.Web.Controllers.Products.DeleteProduct;

[Authorize]
[ApiController]
[Route("api/products")]
public class DeleteProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeleteProductResponseDto>> DeleteProductAsync(int id)
    {
        var command = new DeleteProductCommand { ProductId = id };
        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return result;
    }
}