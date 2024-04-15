using Allspark.Application.UseCases.Assignments.GetPaginationAssignments;
using Allspark.Web.Controllers.Assignments.GetPaginationAssignmentController;

namespace Allspark.Web.Controllers.Assignments;

[ApiController]
[Route("api/assignments")]
public class GetPaginationAssignmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GetPaginationAssignmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginationAssignmentsAsync([FromQuery] GetPaginationFilter filter)
    {
        var query = new GetPaginationAssignmentsQuery
        {
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };

        var pagedResponse = await _mediator.Send(query);

        return Ok(pagedResponse);
    }

}

