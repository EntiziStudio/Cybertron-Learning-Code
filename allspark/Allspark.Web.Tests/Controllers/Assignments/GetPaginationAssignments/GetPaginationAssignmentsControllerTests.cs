using Allspark.Application.UseCases.Assignments.GetPaginationAssignments;
using Allspark.Application.UseCases.Assignments.ResponseDtos;
using Allspark.Application.Wrappers;
using Allspark.Web.Controllers.Assignments;
using Allspark.Web.Controllers.Assignments.GetPaginationAssignmentController;

namespace Allspark.Tests.Web.Controllers.Assignments;

public class PaginationAssignmentsControllerTests
{
    [Fact]
    public async Task GetAssignments_ReturnsOkResponse()
    {
        // Arrange
        var filter = new GetPaginationFilter
        {
            PageNumber = 1,
            PageSize = 5,
        };

        var totalPages = 2;
        var totalRecords = 10;

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<GetPaginationAssignmentsQuery>(), default))
                    .ReturnsAsync(new PagedResponse<IEnumerable<AssignmentResponseDto>>(GetSampleAssignments(),
                        filter.PageNumber,
                        filter.PageSize,
                        totalPages,
                        totalRecords
        ));

        var controller = new GetPaginationAssignmentsController(mockMediator.Object);

        // Act
        var response = await controller.GetPaginationAssignmentsAsync(filter);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(response);
        var resultValue = Assert.IsType<PagedResponse<IEnumerable<AssignmentResponseDto>>>(okResult.Value);

        Assert.NotNull(resultValue);
        Assert.Equal(filter.PageNumber, resultValue.PageNumber);
        Assert.Equal(filter.PageSize, resultValue.PageSize);
        Assert.Equal(totalPages, resultValue.TotalPages);
        Assert.Equal(totalRecords, resultValue.TotalRecords);

        Assert.Equal(totalPages, resultValue.Data!.Count());
        Assert.Equal("Assignment 1", resultValue.Data!.First().Title);
        Assert.Equal("Assignment 2", resultValue.Data!.Skip(1).First().Title);
    }

    private static List<AssignmentResponseDto> GetSampleAssignments()
    {
        return new List<AssignmentResponseDto>
        {
            new AssignmentResponseDto { Id = 111, Title = "Assignment 1" },
            new AssignmentResponseDto { Id = 222, Title = "Assignment 2" }
        };
    }

    [Fact]
    public async Task GetPaginationAssignments_ReturnsValidPaginationData()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 5;

        var expectedAssignments = new List<AssignmentResponseDto>
        {
            new AssignmentResponseDto { Id = 111, Code = "100", Title = "Assignment 1", Description = "Test" },
            new AssignmentResponseDto { Id = 223, Code = "200", Title = "Assignment 2", Description = "Test" },
            new AssignmentResponseDto { Id = 345, Code = "300", Title = "Assignment 3", Description = "Test" },
            new AssignmentResponseDto { Id = 466, Code = "400", Title = "Assignment 4", Description = "Test" },
            new AssignmentResponseDto { Id = 577, Code = "500", Title = "Assignment 5", Description = "Test" },
        };

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(mediator => mediator.Send(It.IsAny<GetPaginationAssignmentsQuery>(), default))
                    .ReturnsAsync(new PagedResponse<IEnumerable<AssignmentResponseDto>>(
                        expectedAssignments,
                        pageNumber,
                        pageSize,
                        1,
                        expectedAssignments.Count
        ));

        var controller = new GetPaginationAssignmentsController(mockMediator.Object);

        // Act
        var filter = new GetPaginationFilter { PageNumber = pageNumber, PageSize = pageSize };
        var result = await controller.GetPaginationAssignmentsAsync(filter);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<PagedResponse<IEnumerable<AssignmentResponseDto>>>(okResult.Value);

        var assignmentList = response.Data!.ToList();
        Assert.Equal(pageSize, assignmentList.Count);

        foreach (var assignment in expectedAssignments)
        {
            var assignmentResponseDto = assignmentList.Find(a => a.Id == assignment.Id);
            Assert.NotNull(assignmentResponseDto);
            Assert.Equal(assignment.Code, assignmentResponseDto.Code);
            Assert.Equal(assignment.Title, assignmentResponseDto.Title);
            Assert.Equal(assignment.Description, assignmentResponseDto.Description);
        }
    }

    [Fact]
    public async Task GetPaginationAssignments_PerformanceTest()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 10;

        var expectedAssignments = Enumerable.Range(1, 100000)
            .Select(i => new AssignmentResponseDto
            {
                Id = i,
                Code = $"Code{i}",
                Title = $"Assignment {i}",
                Description = $"Description {i}"
            });

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(mediator => mediator.Send(It.IsAny<GetPaginationAssignmentsQuery>(), default))
                    .ReturnsAsync(new PagedResponse<IEnumerable<AssignmentResponseDto>>(
                        expectedAssignments,
                        pageNumber,
                        pageSize,
                        1,
                        expectedAssignments.Count()
        ));

        var controller = new GetPaginationAssignmentsController(mockMediator.Object);

        var stopwatch = new Stopwatch();

        // Act
        var filter = new GetPaginationFilter { PageNumber = pageNumber, PageSize = pageSize };

        stopwatch.Start();
        var result = await controller.GetPaginationAssignmentsAsync(filter);
        stopwatch.Stop();

        // Assert
        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        Console.WriteLine($"Execution time: {elapsedMilliseconds} milliseconds");
        Assert.True(elapsedMilliseconds < 1000);
    }

    [Fact]
    public async Task GetPaginationAssignments_ReturnsEmptyList_WhenNoDataForPagination()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 10;

        var emptyList = Enumerable.Empty<AssignmentResponseDto>();

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(mediator => mediator.Send(It.IsAny<GetPaginationAssignmentsQuery>(), default))
                    .ReturnsAsync(new PagedResponse<IEnumerable<AssignmentResponseDto>>(
                        emptyList,
                        pageNumber,
                        pageSize,
                        1,
                        emptyList.Count()
        ));

        var controller = new GetPaginationAssignmentsController(mockMediator.Object);

        var filter = new GetPaginationFilter { PageNumber = pageNumber, PageSize = pageSize };

        // Act
        var result = await controller.GetPaginationAssignmentsAsync(filter);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<PagedResponse<IEnumerable<AssignmentResponseDto>>>(okResult.Value);

        var assignmentList = response.Data!.ToList();
        Assert.Empty(assignmentList);
    }

    [Fact]
    public async Task GetAssignments_ReturnsCorrectTotalPagesAndTotalRecords()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 5;

        var expectedAssignments = new List<AssignmentResponseDto>
        {
            new AssignmentResponseDto { Id = 111, Code = "100", Title = "Assignment 1", Description = "Test" },
            new AssignmentResponseDto { Id = 112, Code = "200", Title = "Assignment 2", Description = "Test" },
            new AssignmentResponseDto { Id = 113, Code = "300", Title = "Assignment 3", Description = "Test" },
            new AssignmentResponseDto { Id = 114, Code = "400", Title = "Assignment 4", Description = "Test" },
            new AssignmentResponseDto { Id = 115, Code = "500", Title = "Assignment 5", Description = "Test" },
        };

        var totalPages = 3;
        var totalRecords = 14;

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(mediator => mediator.Send(It.IsAny<GetPaginationAssignmentsQuery>(), default))
            .ReturnsAsync(new PagedResponse<IEnumerable<AssignmentResponseDto>>(
                expectedAssignments,
                pageNumber,
                pageSize,
                totalPages,
                totalRecords
        ));

        var controller = new GetPaginationAssignmentsController(mockMediator.Object);

        // Act
        var filter = new GetPaginationFilter { PageNumber = pageNumber, PageSize = pageSize };
        var result = await controller.GetPaginationAssignmentsAsync(filter);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<PagedResponse<IEnumerable<AssignmentResponseDto>>>(okResult.Value);

        Assert.NotNull(response);
        Assert.Equal(pageNumber, response.PageNumber);
        Assert.Equal(pageSize, response.PageSize);
        Assert.Equal(totalPages, response.TotalPages);
        Assert.Equal(totalRecords, response.TotalRecords);
        Assert.Equal(pageSize, response.Data!.Count());
    }
}
