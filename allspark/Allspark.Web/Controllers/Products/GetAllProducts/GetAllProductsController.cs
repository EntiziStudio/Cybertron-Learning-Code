using Allspark.Application.UseCases.Products.GetAllProducts;
using Allspark.Application.UseCases.Products.ResponseDtos;

namespace Allspark.Web.Controllers.Products.GetAllProducts;

[Authorize]
[ApiController]
[Route("api/products")]
public class GetAllProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAllProductsAsync()
    {
        var query = new GetAllProductsQuery();
        var products = await _mediator.Send(query);
        return Ok(products);
    }

}