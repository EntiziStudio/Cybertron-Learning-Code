using Allspark.Application.UseCases.Assignments.ResponseDtos;
using Allspark.Application.Wrappers;

namespace Allspark.Application.UseCases.Assignments.GetPaginationAssignments;

public class GetPaginationAssignmentsQuery : IRequest<PagedResponse<IEnumerable<AssignmentResponseDto>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
