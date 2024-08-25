using Application.Common.Interfaces;
using Application.SearchResults.Commands;
using Application.SearchResults.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace Application.UnitTests.SearchResults.Commands
{
    public class CreateSearchResultHandlerTests
    {
        private readonly Mock<IApplicationDbContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateSearchResultHandler _handler;

        public CreateSearchResultHandlerTests()
        {
            _mockContext = new Mock<IApplicationDbContext>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateSearchResultHandler(_mockContext.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldMapAndSaveSearchResult()
        {
            // Arrange
            var searchResultDto = new SearchResultDto
            {
                SearchEngineType = SearchEngineType.Google,
                Keywords = "land registry search",
                TargetUrl = "www.infotrack.co.uk"
            };

            var searchResult = new SearchResult
            {
                // Set properties that match SearchResultDto
                SearchEngineType = SearchEngineType.Google,
                Keywords = "land registry search",
                TargetUrl = "www.infotrack.co.uk"
            };

            _mockMapper.Setup(m => m.Map<SearchResult>(searchResultDto))
                       .Returns(searchResult);

            _mockContext.Setup(c => c.SearchResults.Add(It.IsAny<SearchResult>()))
                        .Verifiable();

            _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(1);

            var command = new CreateSearchResultCommand(searchResultDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(searchResult.Id, result);
            _mockMapper.Verify(m => m.Map<SearchResult>(searchResultDto), Times.Once);
            _mockContext.Verify(c => c.SearchResults.Add(It.Is<SearchResult>(sr => sr.Id == searchResult.Id)), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
