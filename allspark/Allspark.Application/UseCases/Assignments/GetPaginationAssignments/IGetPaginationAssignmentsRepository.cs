using Allspark.Application.UseCases.Assignments.ResponseDtos;
using Allspark.Application.Wrappers;

namespace Allspark.Application.UseCases.Assignments.GetPaginationAssignments;

public interface IGetPaginationAssignmentsRepository 
{
    Task<PagedResponse<IEnumerable<AssignmentResponseDto>>> GetPaginationAssignmentsAsync(int pageNumber, int pageSize);
}
