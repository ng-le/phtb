using Application.Scraping.Interfaces;
using Domain.Enums;
using Infrastructure.Services.Scraping;
using Moq;

namespace Application.UnitTests.Scraping
{
    public class BingSearchServiceTests
    {
        private readonly Mock<IWebScraper> _mockWebScraper;
        private readonly Mock<IHtmlParser> _mockHtmlParser;
        private readonly BingSearchService _bingSearchService;

        public BingSearchServiceTests()
        {
            _mockWebScraper = new Mock<IWebScraper>();
            _mockHtmlParser = new Mock<IHtmlParser>();
            _bingSearchService = new BingSearchService(_mockWebScraper.Object, _mockHtmlParser.Object);
        }

        [Fact]
        public async Task GetUrlPositions_ValidInputs_ReturnsCorrectPositions()
        {
            // Arrange
            string keywords = "example";
            string targetUrl = "https://target.com";
            string bingHtmlContent = "<html>...</html>";
            var parsedUrls = new List<string> { "https://example.com", "https://target.com", "https://other.com" };
            var expectedPositions = "2";

            _mockWebScraper.Setup(s => s.GetPageContent(It.IsAny<string>())).ReturnsAsync(bingHtmlContent);
            _mockHtmlParser.Setup(p => p.ParseUrls(bingHtmlContent, SearchEngineType.Bing)).Returns(parsedUrls);

            // Act
            var result = await _bingSearchService.GetUrlPositions(keywords, targetUrl);

            // Assert
            Assert.Equal(expectedPositions, result);
        }

        [Fact]
        public async void GetUrlPositions_TargetUrlNotFound_ReturnsZero()
        {
            // Arrange
            string keywords = "example";
            string targetUrl = "https://notfound.com";
            string bingHtmlContent = "<html>...</html>";
            var parsedUrls = new List<string> { "https://example.com", "https://other.com" };
            var expectedPositions = "0";

            _mockWebScraper.Setup(s => s.GetPageContent(It.IsAny<string>())).ReturnsAsync(bingHtmlContent);
            _mockHtmlParser.Setup(p => p.ParseUrls(bingHtmlContent, SearchEngineType.Bing)).Returns(parsedUrls);

            // Act
            var result = await _bingSearchService.GetUrlPositions(keywords, targetUrl);

            // Assert
            Assert.Equal(expectedPositions, result);
        }
    }
}
