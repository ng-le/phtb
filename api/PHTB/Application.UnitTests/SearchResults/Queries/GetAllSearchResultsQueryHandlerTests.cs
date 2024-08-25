using Application.Common.Interfaces;
using Application.SearchResults.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Moq;
using Moq.EntityFrameworkCore;

namespace Application.UnitTests.SearchResults.Queries
{
    public class GetAllSearchResultsQueryHandlerTests
    {
        private readonly Mock<IApplicationDbContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetAllSearchResultsQueryHandler _handler;

        public GetAllSearchResultsQueryHandlerTests()
        {
            _mockContext = new Mock<IApplicationDbContext>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetAllSearchResultsQueryHandler(_mockContext.Object, _mockMapper.Object);

            // Seed the mock DbContext
            SeedMockDbContext();
        }

        private void SeedMockDbContext()
        {
            var searchResults = new List<SearchResult>
            {
                new SearchResult
                {
                    Id = Guid.NewGuid(),
                    Keywords = "Keywords1",
                    TargetUrl = "TargetUrl1",
                    SearchEngineType = SearchEngineType.Google,
                    Result = "1",
                    CreatedOn = DateTime.UtcNow.AddDays(-1)
                },
                new SearchResult
                {
                    Id = Guid.NewGuid(),
                    Keywords = "Keywords2",
                    TargetUrl = "TargetUrl2",
                    SearchEngineType = SearchEngineType.Google,
                    Result = "2",
                    CreatedOn = DateTime.UtcNow.AddDays(-2)
                }
            };

            // Mock the SearchResults DbSet with Moq.EntityFrameworkCore
            _mockContext.Setup(c => c.SearchResults).ReturnsDbSet(searchResults);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfSearchResultDto()
        {
            // Arrange
            var searchResultDtos = new List<SearchResultDto>
            {
                new SearchResultDto
                {
                    Keywords = "Keywords1",
                    TargetUrl = "TargetUrl1",
                    SearchEngineType = SearchEngineType.Google,
                    Result = "1",
                },
                new SearchResultDto
                {
                    Keywords = "Keywords2",
                    TargetUrl = "TargetUrl2",
                    SearchEngineType = SearchEngineType.Google,
                    Result = "2",
                }
            };

            _mockMapper.Setup(m => m.Map<List<SearchResultDto>>(It.IsAny<List<SearchResult>>()))
                       .Returns(searchResultDtos);

            var query = new GetAllSearchResultsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Keywords1", result[0].Keywords);
            Assert.Equal("Keywords2", result[1].Keywords);

            _mockMapper.Verify(m => m.Map<List<SearchResultDto>>(It.IsAny<List<SearchResult>>()), Times.Once);
        }
    }
}
