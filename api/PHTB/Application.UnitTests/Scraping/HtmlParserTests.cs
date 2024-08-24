using Domain.Enums;
using Infrastructure.Services.Scraping;

namespace Application.UnitTests.Scraping
{
    public class HtmlParserTests
    {
        private readonly HtmlParser _htmlParser;

        public HtmlParserTests()
        {
            _htmlParser = new HtmlParser();
        }

        [Fact]
        public void ParseUrls_GoogleSearchEngine_ParsesUrlsCorrectly()
        {
            // Arrange
            var htmlContent = @"
                <a href=""/url?q=https://example.com&sa=U&ved=2ahUKEwi&usg=AOvVaw0O"">
                <a href=""/url?q=https://anotherexample.com&sa=U&ved=2ahUKEwi&usg=AOvVaw1P"">
            ";
            var expectedUrls = new List<string>
            {
                "https://example.com",
                "https://anotherexample.com"
            };

            // Act
            var result = _htmlParser.ParseUrls(htmlContent, SearchEngineType.Google);

            // Assert
            Assert.Equal(expectedUrls, result);
        }

        [Fact]
        public void ParseUrls_BingSearchEngine_ParsesUrlsCorrectly()
        {
            // Arrange
            var htmlContent = @"
                <a href=""https://example.com"">Example</a>
                <a href=""https://anotherexample.com"">Another Example</a>
            ";
            var expectedUrls = new List<string>
            {
                "https://example.com",
                "https://anotherexample.com"
            };

            // Act
            var result = _htmlParser.ParseUrls(htmlContent, SearchEngineType.Bing);

            // Assert
            Assert.Equal(expectedUrls, result);
        }

        [Fact]
        public void ParseUrls_UnsupportedSearchEngineType_ThrowsArgumentException()
        {
            // Arrange
            var htmlContent = "<a href=\"https://example.com\">Example</a>";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _htmlParser.ParseUrls(htmlContent, (SearchEngineType)999));
        }

        [Fact]
        public void ParseUrls_GoogleSearchEngine_NoUrlsFound_ReturnsEmptyList()
        {
            // Arrange
            var htmlContent = "<p>No links here!</p>";

            // Act
            var result = _htmlParser.ParseUrls(htmlContent, SearchEngineType.Google);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void ParseUrls_BingSearchEngine_NoUrlsFound_ReturnsEmptyList()
        {
            // Arrange
            var htmlContent = "<p>No links here!</p>";

            // Act
            var result = _htmlParser.ParseUrls(htmlContent, SearchEngineType.Bing);

            // Assert
            Assert.Empty(result);
        }

    }
}
