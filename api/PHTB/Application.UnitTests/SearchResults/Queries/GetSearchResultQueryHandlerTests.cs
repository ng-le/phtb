using Application.Scraping.Interfaces;
using Application.SearchResults.Commands;
using Application.SearchResults.Queries;
using Domain.Enums;
using MediatR;
using Moq;

namespace Application.UnitTests.SearchResults.Queries
{
    public class GetSearchResultQueryHandlerTests
    {
        private readonly Mock<ISearchServiceFactory> _mockSearchServiceFactory;
        private readonly Mock<ISearchService> _mockSearchService;
        private readonly Mock<ISender> _mockMediator;
        private readonly GetSearchResultQueryHandler _handler;

        public GetSearchResultQueryHandlerTests()
        {
            _mockSearchServiceFactory = new Mock<ISearchServiceFactory>();
            _mockSearchService = new Mock<ISearchService>();
            _mockMediator = new Mock<ISender>();

            _handler = new GetSearchResultQueryHandler(_mockSearchServiceFactory.Object, _mockMediator.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSearchResultDto()
        {
            // Arrange
            var keywords = "land registry search";
            var targetUrl = "www.infotrack.co.uk";
            var searchEngineType = SearchEngineType.Google;

            var urlPositions = "1,5,10";

            _mockSearchServiceFactory.Setup(f => f.Create(searchEngineType))
                                     .Returns(_mockSearchService.Object);

            _mockSearchService.Setup(s => s.GetUrlPositions(keywords, targetUrl))
                              .ReturnsAsync(urlPositions);

            _mockMediator.Setup(m => m.Send(It.IsAny<CreateSearchResultCommand>(), It.IsAny<CancellationToken>()))
                         .Verifiable();

            var query = new GetSearchResultQuery(keywords, targetUrl, searchEngineType);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(searchEngineType, result.SearchEngineType);
            Assert.Equal(keywords, result.Keywords);
            Assert.Equal(targetUrl, result.TargetUrl);
            Assert.Equal(urlPositions, result.Result);
            Assert.Equal(DateTime.UtcNow.Date, result.CreatedOn.Date); // Checking only the date part

            _mockSearchServiceFactory.Verify(f => f.Create(searchEngineType), Times.Once);
            _mockSearchService.Verify(s => s.GetUrlPositions(keywords, targetUrl), Times.Once);
            _mockMediator.Verify(m => m.Send(It.Is<CreateSearchResultCommand>(cmd => cmd.SearchResult == result), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldCallMediatorSendWithCorrectCommand()
        {
            // Arrange
            var keywords = "land registry search";
            var targetUrl = "www.infotrack.co.uk";
            var searchEngineType = SearchEngineType.Google;

            var urlPositions = "1,5,10";

            _mockSearchServiceFactory.Setup(f => f.Create(searchEngineType))
                                     .Returns(_mockSearchService.Object);

            _mockSearchService.Setup(s => s.GetUrlPositions(keywords, targetUrl))
                              .ReturnsAsync(urlPositions);

            _mockMediator.Setup(m => m.Send(It.IsAny<CreateSearchResultCommand>(), It.IsAny<CancellationToken>()))
                         .Verifiable();

            var query = new GetSearchResultQuery(keywords, targetUrl, searchEngineType);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            _mockMediator.Verify(m => m.Send(It.Is<CreateSearchResultCommand>(cmd =>
                cmd.SearchResult.SearchEngineType == searchEngineType &&
                cmd.SearchResult.Keywords == keywords &&
                cmd.SearchResult.TargetUrl == targetUrl &&
                cmd.SearchResult.Result == urlPositions),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
