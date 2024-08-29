using Application.Common.Exceptions;
using Infrastructure.Services.Scraping;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;

namespace Application.UnitTests.Scraping
{
    public class WebScraperTests
    {
        private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;
        private readonly Mock<ILogger<WebScraper>> _mockLogger;
        private readonly WebScraper _webScraper;

        public WebScraperTests()
        {
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _mockLogger = new Mock<ILogger<WebScraper>>();
            _webScraper = new WebScraper(_mockHttpClientFactory.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetPageContent_WhenResponseSuccessfully_ThenReturnsHtmlContent()
        {
            // Arrange
            var expectedHtmlContent = "<html><body>Example Content</body></html>";

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(expectedHtmlContent),
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);

            _mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            // Act
            var result = await _webScraper.GetPageContent("https://example.com");

            // Assert
            Assert.Equal(expectedHtmlContent, result);
        }

        [Fact]
        public async Task GetPageContent_WhenThrowHttpRequestException_ThenLogsErrorAndThrowWebScraperException()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ThrowsAsync(new HttpRequestException("Network error"));

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);

            _mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            // Act
            await Assert.ThrowsAsync<WebScraperException>(async () => await _webScraper.GetPageContent("https://example.com"));

            // Assert
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Get page content from url exception: Network error")),
                    It.IsAny<HttpRequestException>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
