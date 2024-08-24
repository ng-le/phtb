using Application.Scraping.Interfaces;
using Domain.Enums;

namespace Infrastructure.Services.Scraping
{
    public class BingSearchService : SearchServiceBase
    {
        public BingSearchService(IWebScraper webScraper, IHtmlParser htmlParser)
        : base(webScraper, htmlParser)
        {
        }

        protected override string BuildSearchUrl(string keywords)
        {
            return $"https://www.bing.com/search?num=100&q={Uri.EscapeDataString(keywords)}";
        }

        protected override SearchEngineType GetSearchEngineType()
        {
            return SearchEngineType.Bing;
        }
    }
}
