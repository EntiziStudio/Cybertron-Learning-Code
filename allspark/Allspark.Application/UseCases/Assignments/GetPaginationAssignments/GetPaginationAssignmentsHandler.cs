using Allspark.Application.UseCases.Assignments.ResponseDtos;
using Allspark.Application.Wrappers;

namespace Allspark.Application.UseCases.Assignments.GetPaginationAssignments;

public class GetPaginationAssignmentsHandler : IRequestHandler<GetPaginationAssignmentsQuery, PagedResponse<IEnumerable<AssignmentResponseDto>>>
{
    private readonly IGetPaginationAssignmentsRepository _getPaginationAssignmentsRepository;

    public GetPaginationAssignmentsHandler(IGetPaginationAssignmentsRepository getPaginationAssignmentsRepository)

    {
        _getPaginationAssignmentsRepository = getPaginationAssignmentsRepository ?? throw new ArgumentNullException(nameof(getPaginationAssignmentsRepository));
    }

    public async Task<PagedResponse<IEnumerable<AssignmentResponseDto>>> Handle(GetPaginationAssignmentsQuery request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            throw new OperationCanceledException();
        }
        
        var pagedResponse = await _getPaginationAssignmentsRepository.GetPaginationAssignmentsAsync(request.PageNumber, request.PageSize);

        return pagedResponse;
    }
}
