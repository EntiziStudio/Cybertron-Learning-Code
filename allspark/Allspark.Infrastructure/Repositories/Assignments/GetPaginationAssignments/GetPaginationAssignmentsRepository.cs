using Allspark.Application.UseCases.Assignments.GetPaginationAssignments;
using Allspark.Application.UseCases.Assignments.ResponseDtos;
using Allspark.Application.Wrappers;
using Allspark.Domain.Entities;
using Allspark.Infrastructure.Data;

namespace Allspark.Infrastructure.Repositories.Assignments.GetPaginationAssignments;

public class GetPaginationAssignmentsRepository : IGetPaginationAssignmentsRepository
{
    private readonly AllsparkDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPaginationAssignmentsRepository(AllsparkDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper;
    }

    public async Task<PagedResponse<IEnumerable<AssignmentResponseDto>>> GetPaginationAssignmentsAsync(int pageNumber, int pageSize)
    {
        var assignmentsQuery = _dbContext.Assignments
            .Where(c => c.Deleted == false);

        var totalRecords = await assignmentsQuery.CountAsync();

        var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

        var assignments = await assignmentsQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var assignmentResponse = _mapper.Map<List<Assignment>, IEnumerable<AssignmentResponseDto>>(assignments);

        return new PagedResponse<IEnumerable<AssignmentResponseDto>>(assignmentResponse, pageNumber, pageSize, totalPages, totalRecords);
    }
}
