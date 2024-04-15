using Allspark.Application.UseCases.Assignments.GetPaginationAssignments;
using Allspark.Application.UseCases.Assignments.ResponseDtos;
using Allspark.Application.Wrappers;
using Allspark.Domain.Entities;
using Allspark.Infrastructure.Data;

namespace Allspark.Tests.Application.UseCases.Assignments.GetPaginationAssignments;

public class GetPaginationAssignmentsRepositoryTests
{
    [Fact]
    public async Task Handle_ReturnsPagedAssignments_WhenCalled()
    {
        // Arrange
        var expectedAssignments = new List<Assignment>
        {
            new Assignment { Id = 111, Code = "100", Title = "Assignment 1", Deleted = false },
            new Assignment { Id = 222, Code = "200", Title = "Assignment 2", Deleted = false },
            new Assignment { Id = 333, Code = "300", Title = "Assignment 3", Deleted = false },
            new Assignment { Id = 444, Code = "400", Title = "Assignment 4", Deleted = false },
            new Assignment { Id = 555, Code = "500", Title = "Assignment 5", Deleted = false },
        };

        var pageNumber = 1;
        var pageSize = 5;

        var totalPages = 1;
        var totalRecords = 5;

        var expectedResponse = expectedAssignments.Select(a => new AssignmentResponseDto
        {
            Id = a.Id,
            Code = a.Code,
            Title = a.Title,
            Description = a.Description
        }).ToList();

        var dbContextOptions = new DbContextOptionsBuilder<AllsparkDbContext>()
            .UseInMemoryDatabase(databaseName: "AssignmentDatabase")
            .Options;
        var dbContext = new AllsparkDbContext(dbContextOptions);
        {
            dbContext.Assignments.AddRange(expectedAssignments);
            dbContext.SaveChanges();

            var mockRepository = new Mock<IGetPaginationAssignmentsRepository>();
            mockRepository.Setup(repo => repo.GetPaginationAssignmentsAsync(pageNumber, pageSize))
                .ReturnsAsync(new PagedResponse<IEnumerable<AssignmentResponseDto>>(expectedResponse, pageNumber, pageSize, totalPages, totalRecords));

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Assignment, AssignmentResponseDto>();
            });
            var mapper = mapperConfig.CreateMapper();

            var handler = new GetPaginationAssignmentsHandler(mockRepository.Object);

            var request = new GetPaginationAssignmentsQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await handler.Handle(request, cancellationToken);

            // Assert
            Assert.Equal(expectedResponse, result.Data!);
            Assert.NotNull(result.Data);
            Assert.Equal(pageSize, result.Data.Count());
            Assert.All(result.Data, a => Assert.IsType<AssignmentResponseDto>(a));
        }
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenNoAssignmentsFound()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 3;

        var totalPages = 0;
        var totalRecords = 0;

        var mockRepository = new Mock<IGetPaginationAssignmentsRepository>();

        var handler = new GetPaginationAssignmentsHandler(mockRepository.Object);

        var request = new GetPaginationAssignmentsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var cancellationToken = CancellationToken.None;

        // Act
        var expectedResponse = new PagedResponse<IEnumerable<AssignmentResponseDto>>(new List<AssignmentResponseDto>(), pageNumber, pageSize, totalPages, totalRecords);
        mockRepository.Setup(repo => repo.GetPaginationAssignmentsAsync(pageNumber, pageSize))
            .ReturnsAsync(expectedResponse);

        var result = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result.Data);
        Assert.Empty(result.Data);
        Assert.Equal(totalRecords, result.TotalRecords);
        Assert.Equal(totalPages, result.TotalPages);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenPageNumberExceedsTotalPages()
    {
        // Arrange
        var pageNumber = 999;
        var pageSize = 3;

        var totalPages = 0;
        var totalRecords = 0;

        var expectedResponse = new PagedResponse<IEnumerable<AssignmentResponseDto>>(new List<AssignmentResponseDto>(), pageNumber, pageSize, totalPages, totalRecords);

        var mockRepository = new Mock<IGetPaginationAssignmentsRepository>();
        mockRepository.Setup(repo => repo.GetPaginationAssignmentsAsync(pageNumber, pageSize))
            .ReturnsAsync(expectedResponse);

        var handler = new GetPaginationAssignmentsHandler(mockRepository.Object);

        var request = new GetPaginationAssignmentsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result.Data);
        Assert.Empty(result.Data);
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
            new AssignmentResponseDto { Id = 121, Code = "100", Title = "Assignment 1", Description = "test" },
            new AssignmentResponseDto { Id = 322, Code = "200", Title = "Assignment 2", Description = "test" },
            new AssignmentResponseDto { Id = 223, Code = "300", Title = "Assignment 3", Description = "test" },
            new AssignmentResponseDto { Id = 444, Code = "400", Title = "Assignment 4", Description = "test" },
            new AssignmentResponseDto { Id = 125, Code = "500", Title = "Assignment 5", Description = "test" },
            new AssignmentResponseDto { Id = 333, Code = "700", Title = "Assignment 6", Description = "test" },
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
        Assert.NotNull(result.Data);
        Assert.Equal(totalRecords, result.Data.Count());

        foreach (var assignment in expectedAssignments)
        {
            Assert.Contains(assignment, result.Data!);
        }

        var expectedTotalPages = (int)Math.Ceiling((double)expectedAssignments.Count / pageSize);
        Assert.Equal(expectedTotalPages, result.TotalPages);
        Assert.Equal(totalRecords, result.TotalRecords);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenNoDataForPagination()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 10;

        var totalPages = 0;
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
        Assert.NotNull(result.Data);
        Assert.Empty(result.Data);
        Assert.Equal(totalRecords, result.TotalRecords);
        Assert.Equal(totalPages, result.TotalPages);
    }
}
