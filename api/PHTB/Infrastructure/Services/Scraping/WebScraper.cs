using Application.Common.Exceptions;
using Application.Scraping.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Scraping
{
    public class WebScraper : IWebScraper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<WebScraper> _logger;
        public WebScraper(IHttpClientFactory httpClientFactory, ILogger<WebScraper> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<string> GetPageContent(string url)
        {
            var httpClient = _httpClientFactory.CreateClient();            

            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Get page content from url exception: {ex.Message}");
                throw new WebScraperException("Failed to get page content from the URL: " + ex.Message);
            }
        }
    }
}
