using Allspark.Application.UseCases.Products.ResponseDtos;
using Allspark.Application.UseCases.Products.UpdateProduct;

namespace Allspark.Web.Controllers.Products.UpdateProduct;

[Authorize]
[ApiController]
[Route("api/products")]
public class UpdateProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductResponseDto>> UpdateProductAsync(int id, UpdateProductCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return result;
    }
}