using Application.Scraping.Interfaces;
using Domain.Enums;
using Infrastructure.Services.Scraping;
using Moq;

namespace Application.UnitTests.Scraping
{
    public class GoogleSearchServiceTests
    {
        private readonly Mock<IWebScraper> _mockWebScraper;
        private readonly Mock<IHtmlParser> _mockHtmlParser;
        private readonly GoogleSearchService _googleSearchService;

        public GoogleSearchServiceTests()
        {
            _mockWebScraper = new Mock<IWebScraper>();
            _mockHtmlParser = new Mock<IHtmlParser>();
            _googleSearchService = new GoogleSearchService(_mockWebScraper.Object, _mockHtmlParser.Object);
        }

        [Fact]
        public async Task GetUrlPositions_ValidInputs_ReturnsCorrectPositions()
        {
            // Arrange
            string keywords = "example";
            string targetUrl = "https://target.com";
            string googleHtmlContent = "<html>...</html>";
            var parsedUrls = new List<string> { "https://example.com", "https://target.com", "https://other.com" };
            var expectedPositions = "2";

            _mockWebScraper.Setup(s => s.GetPageContent(It.IsAny<string>())).ReturnsAsync(googleHtmlContent);
            _mockHtmlParser.Setup(p => p.ParseUrls(googleHtmlContent, SearchEngineType.Google)).Returns(parsedUrls);

            // Act
            var result = await _googleSearchService.GetUrlPositions(keywords, targetUrl);

            // Assert
            Assert.Equal(expectedPositions, result);
        }

        [Fact]
        public async Task GetUrlPositions_TargetUrlNotFound_ReturnsZero()
        {
            // Arrange
            string keywords = "example";
            string targetUrl = "https://notfound.com";
            string googleHtmlContent = "<html>...</html>";
            var parsedUrls = new List<string> { "https://example.com", "https://other.com" };
            var expectedPositions = "0";

            _mockWebScraper.Setup(s => s.GetPageContent(It.IsAny<string>())).ReturnsAsync(googleHtmlContent);
            _mockHtmlParser.Setup(p => p.ParseUrls(googleHtmlContent, SearchEngineType.Google)).Returns(parsedUrls);

            // Act
            var result = await _googleSearchService.GetUrlPositions(keywords, targetUrl);

            // Assert
            Assert.Equal(expectedPositions, result);
        }
    }
}
