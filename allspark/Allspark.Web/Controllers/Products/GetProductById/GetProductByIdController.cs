using Allspark.Application.UseCases.Products.GetProductById;
using Allspark.Application.UseCases.Products.ResponseDtos;

namespace Allspark.Web.Controllers.Products.GetProductById;

[ApiController]
[Route("api/products")]
public class GetProductByIdController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetProductByIdController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDto>> GetProductByIdAsync(int id)
    {
        var query = new GetProductByIdQuery { Id = id };
        var product = await _mediator.Send(query);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }
}