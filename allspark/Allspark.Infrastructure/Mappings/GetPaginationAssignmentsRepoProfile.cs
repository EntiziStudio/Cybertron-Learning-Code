using Allspark.Application.UseCases.Assignments.ResponseDtos;
using Allspark.Domain.Entities;

namespace Allspark.Infrastructure.Mappings;

public class GetPaginationAssignmentsRepoProfile : Profile
{
    public GetPaginationAssignmentsRepoProfile() 
    {
        CreateMap<Assignment, AssignmentResponseDto>();
    }   
}
