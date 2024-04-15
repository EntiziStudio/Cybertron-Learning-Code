using Allspark.Application.UseCases.Assignments.GetPaginationAssignments;
using Allspark.Application.UseCases.Assignments.ResponseDtos;
using Allspark.Application.Wrappers;

namespace Allspark.Tests.Application.UseCases.Assignments.GetPaginationAssignments;

public class PaginationAssignmentsHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAssignments_WhenCalled()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 3;

        var totalPages = 2;
        var totalRecords = 6;

        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();

        var expectedAssignments = new List<AssignmentResponseDto>
        {
            new AssignmentResponseDto { Id = 111, Code = "100", Title = "Assignment 1", Description = "test" },
            new AssignmentResponseDto { Id = 122, Code = "200", Title = "Assignment 2", Description = "test" },
            new AssignmentResponseDto { Id = 133, Code = "300", Title = "Assignment 3", Description = "test" },
            new AssignmentResponseDto { Id = 144, Code = "400", Title = "Assignment 4", Description = "test" },
            new AssignmentResponseDto { Id = 155, Code = "500", Title = "Assignment 5", Description = "test" },
            new AssignmentResponseDto { Id = 166, Code = "600", Title = "Assignment 6", Description = "test" },
        };

        var mockRepository = new Mock<IGetPaginationAssignmentsRepository>();
        mockRepository.Setup(repo => repo.GetPaginationAssignmentsAsync(pageNumber, pageSize))
                      .ReturnsAsync(new PagedResponse<IEnumerable<AssignmentResponseDto>>(expectedAssignments, pageNumber, pageSize, totalPages, totalRecords));

        var handler = new GetPaginationAssignmentsHandler(mockRepository.Object);

        var request = new GetPaginationAssignmentsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await handler.Handle(request, cancellationToken);

        var resultList = result.Data!.ToList();
        // Assert
        Assert.Equal(totalRecords, resultList.Count);
        for (int i = 0; i < totalRecords; i++)
        {
            Assert.Equal(expectedAssignments[i].Id, resultList[i].Id);
            Assert.Equal(expectedAssignments[i].Code, resultList[i].Code);
            Assert.Equal(expectedAssignments[i].Title, resultList[i].Title);
            Assert.Equal(expectedAssignments[i].Description, resultList[i].Description);
        }

        // Verify
        mockRepository.Verify(repo => repo.GetPaginationAssignmentsAsync(pageNumber, pageSize), Times.Once);

        // Act and Assert
        await Assert.ThrowsAsync<OperationCanceledException>(() => handler.Handle(request, cancellationTokenSource.Token));
    }

    [Fact]
    public async Task Handle_ReturnsDistinctAndCompleteAssignments_WhenCalled()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 5;

        var totalPages = 2;
        var totalRecords = 6;

        var expectedAssignments = new List<AssignmentResponseDto>
        {
            new AssignmentResponseDto { Id = 111, Code = "100", Title = "Assignment 1", Description = "test" },
            new AssignmentResponseDto { Id = 222, Code = "200", Title = "Assignment 2", Description = "test" },
            new AssignmentResponseDto { Id = 344, Code = "300", Title = "Assignment 3", Description = "test" },
            new AssignmentResponseDto { Id = 432, Code = "400", Title = "Assignment 4", Description = "test" },
            new AssignmentResponseDto { Id = 544, Code = "500", Title = "Assignment 5", Description = "test" },
            new AssignmentResponseDto { Id = 613, Code = "600", Title = "Assignment 6", Description = "test" },
        };

        var mockRepository = new Mock<IGetPaginationAssignmentsRepository>();
        mockRepository.Setup(repo => repo.GetPaginationAssignmentsAsync(pageNumber, pageSize))
                      .ReturnsAsync(new PagedResponse<IEnumerable<AssignmentResponseDto>>(expectedAssignments, pageNumber, pageSize, totalPages, totalRecords));

        var handler = new GetPaginationAssignmentsHandler(mockRepository.Object);

        var request = new GetPaginationAssignmentsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        var resultList = result.Data!.ToList();

        Assert.Equal(totalRecords, resultList.Count);

        foreach (var assignment in expectedAssignments)
        {
            Assert.Contains(assignment, resultList);
        }
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenNoDataForPagination()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 10;

        var totalPages = 1;
        var totalRecords = 0;

        var expectedAssignments = new List<AssignmentResponseDto>();

        var mockRepository = new Mock<IGetPaginationAssignmentsRepository>();
        mockRepository.Setup(repo => repo.GetPaginationAssignmentsAsync(pageNumber, pageSize))
                      .ReturnsAsync(new PagedResponse<IEnumerable<AssignmentResponseDto>>(expectedAssignments, pageNumber, pageSize, totalPages, totalRecords));

        var handler = new GetPaginationAssignmentsHandler(mockRepository.Object);

        var request = new GetPaginationAssignmentsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Data!);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenPageNumberExceedsTotalPages()
    {
        // Arrange
        var pageNumber = 10;
        var pageSize = 10;

        var totalPages = 1;
        var totalRecords = 10;

        var expectedAssignments = new List<AssignmentResponseDto>();

        var mockRepository = new Mock<IGetPaginationAssignmentsRepository>();
        mockRepository.Setup(repo => repo.GetPaginationAssignmentsAsync(pageNumber, pageSize))
                      .ReturnsAsync(new PagedResponse<IEnumerable<AssignmentResponseDto>>(expectedAssignments, pageNumber, pageSize, totalPages, totalRecords));

        var handler = new GetPaginationAssignmentsHandler(mockRepository.Object);

        var request = new GetPaginationAssignmentsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Data!);
    }

    [Fact]
    public async Task Handle_ReturnsCorrectTotalPagesAndTotalRecords()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 5;

        var expectedAssignments = new List<AssignmentResponseDto>
        {
            new AssignmentResponseDto { Id = 198, Code = "100", Title = "Assignment 1", Description = "test" },
            new AssignmentResponseDto { Id = 122, Code = "200", Title = "Assignment 2", Description = "test" },
            new AssignmentResponseDto { Id = 333, Code = "300", Title = "Assignment 3", Description = "test" },
            new AssignmentResponseDto { Id = 413, Code = "400", Title = "Assignment 4", Description = "test" },
            new AssignmentResponseDto { Id = 501, Code = "500", Title = "Assignment 5", Description = "test" },
        };

        var totalRecords = 5;
        var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

        var mockRepository = new Mock<IGetPaginationAssignmentsRepository>();
        mockRepository.Setup(repo => repo.GetPaginationAssignmentsAsync(pageNumber, pageSize))
            .ReturnsAsync(new PagedResponse<IEnumerable<AssignmentResponseDto>>(expectedAssignments, pageNumber, pageSize, totalPages, totalRecords));

        var handler = new GetPaginationAssignmentsHandler(mockRepository.Object);

        var request = new GetPaginationAssignmentsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(totalRecords, result.Data!.Count());

        foreach (var assignment in expectedAssignments)
        {
            Assert.Contains(assignment, result.Data!);
        }

        Assert.Equal(totalRecords, result.TotalRecords);
        Assert.Equal(totalPages, result.TotalPages);
    }

    [Fact]
    public async Task Handle_ReturnsAllRecords_WhenPageSizeIsGreaterThanTotalRecords()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 100;

        var totalPages = 1;
        var totalRecords = 6;

        var expectedAssignments = new List<AssignmentResponseDto>
        {
            new AssignmentResponseDto { Id = 162, Code = "100", Title = "Assignment 1", Description = "test" },
            new AssignmentResponseDto { Id = 232, Code = "200", Title = "Assignment 2", Description = "test" },
            new AssignmentResponseDto { Id = 356, Code = "300", Title = "Assignment 3", Description = "test" },
            new AssignmentResponseDto { Id = 477, Code = "400", Title = "Assignment 4", Description = "test" },
            new AssignmentResponseDto { Id = 545, Code = "500", Title = "Assignment 5", Description = "test" },
            new AssignmentResponseDto { Id = 676, Code = "600", Title = "Assignment 6", Description = "test" },
        };

        var mockRepository = new Mock<IGetPaginationAssignmentsRepository>();
        mockRepository.Setup(repo => repo.GetPaginationAssignmentsAsync(pageNumber, pageSize))
            .ReturnsAsync(new PagedResponse<IEnumerable<AssignmentResponseDto>>(expectedAssignments, pageNumber, pageSize, totalPages, totalRecords));

        var handler = new GetPaginationAssignmentsHandler(mockRepository.Object);

        var request = new GetPaginationAssignmentsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(totalRecords, result.Data!.Count());

        foreach (var assignment in expectedAssignments)
        {
            Assert.Contains(assignment, result.Data!);
        }

        Assert.Equal(totalRecords, result.TotalRecords);
        Assert.Equal(totalPages, result.TotalPages);
    }

    [Fact]
    public async Task Handle_ReturnsCorrectTotalPages_WhenTotalRecordsNotDivisibleByPageSize()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 5;

        var totalPages = 2;
        var totalRecords = 6;

        var expectedAssignments = new List<AssignmentResponseDto>
        {
            new AssignmentResponseDto { Id = 111, Code = "100", Title = "Assignment 1", Description = "test" },
            new AssignmentResponseDto { Id = 222, Code = "200", Title = "Assignment 2", Description = "test" },
            new AssignmentResponseDto { Id = 333, Code = "300", Title = "Assignment 3", Description = "test" },
            new AssignmentResponseDto { Id = 444, Code = "400", Title = "Assignment 4", Description = "test" },
            new AssignmentResponseDto { Id = 555, Code = "500", Title = "Assignment 5", Description = "test" },
            new AssignmentResponseDto { Id = 666, Code = "600", Title = "Assignment 6", Description = "test" },
        };

        var mockRepository = new Mock<IGetPaginationAssignmentsRepository>();
        mockRepository.Setup(repo => repo.GetPaginationAssignmentsAsync(pageNumber, pageSize))
                      .ReturnsAsync(new PagedResponse<IEnumerable<AssignmentResponseDto>>(expectedAssignments, pageNumber, pageSize, totalPages, totalRecords));

        var handler = new GetPaginationAssignmentsHandler(mockRepository.Object);

        var request = new GetPaginationAssignmentsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(totalRecords, result.Data!.Count());

        foreach (var assignment in expectedAssignments)
        {
            Assert.Contains(assignment, result.Data!);
        }

        var expectedTotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        Assert.Equal(expectedTotalPages, result.TotalPages);
        Assert.Equal(totalRecords, result.TotalRecords);
    }
}
