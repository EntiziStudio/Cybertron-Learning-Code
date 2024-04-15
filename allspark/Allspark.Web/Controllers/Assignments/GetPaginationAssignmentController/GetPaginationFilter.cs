namespace Allspark.Web.Controllers.Assignments.GetPaginationAssignmentController;

public class GetPaginationFilter
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public GetPaginationFilter()
    {
        PageNumber = 1;
        PageSize = 5;
    }

    public GetPaginationFilter(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize > 5 ? 5 : pageSize;
    }
}
