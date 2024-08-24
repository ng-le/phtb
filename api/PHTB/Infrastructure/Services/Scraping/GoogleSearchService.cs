using Application.Scraping.Interfaces;
using Domain.Enums;

namespace Infrastructure.Services.Scraping
{
    public class GoogleSearchService : SearchServiceBase
    {
        public GoogleSearchService(IWebScraper webScraper, IHtmlParser htmlParser)
        : base(webScraper, htmlParser)
        {
        }
        protected override string BuildSearchUrl(string keywords)
        {
            return $"https://www.google.co.uk/search?num=100&q={Uri.EscapeDataString(keywords)}";
        }

        protected override SearchEngineType GetSearchEngineType()
        {
            return SearchEngineType.Google;
        }
    }
}
