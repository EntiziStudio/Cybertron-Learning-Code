using Allspark.Application.UseCases.Products.CreateProduct;
using Allspark.Application.UseCases.Products.ResponseDtos;

namespace Allspark.Web.Controllers.Products.CreateProduct;

[Authorize(Roles = "111, 112")]
[ApiController]
[Route("api/products")]
public class CreateProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> CreateProductAsync(CreateProductCommand command)
    {
        var createdProduct = await _mediator.Send(command);

        //Return the product obtained by the created product ID using GetProductByIdAsync method
        return createdProduct;
    }
}