using Application.Scraping.Interfaces;
using Domain.Enums;

namespace Infrastructure.Services.Scraping
{
    public abstract class SearchServiceBase : ISearchService
    {
        private readonly IWebScraper _webScraper;
        private readonly IHtmlParser _htmlParser;

        protected SearchServiceBase(IWebScraper webScraper, IHtmlParser htmlParser)
        {
            _webScraper = webScraper;
            _htmlParser = htmlParser;
        }
        public async Task<string> GetUrlPositions(string keywords, string targetUrl)
        {
            var searchUrl = BuildSearchUrl(keywords);
            var htmlContent = await _webScraper.GetPageContent(searchUrl);
            var urls = _htmlParser.ParseUrls(htmlContent, GetSearchEngineType());

            return FindUrlPositions(urls, targetUrl);
        }
        protected abstract string BuildSearchUrl(string keywords);
        protected abstract SearchEngineType GetSearchEngineType();
        private string FindUrlPositions(List<string> urls, string targetUrl)
        {
            var urlPositions = new List<int>();

            for (int i = 0; i < urls.Count; i++)
            {
                if (urls[i].Contains(targetUrl, StringComparison.OrdinalIgnoreCase))
                {
                    urlPositions.Add(i + 1);
                }
            }

            if (urlPositions.Count == 0)
            {
                urlPositions.Add(0);
            }

            return string.Join(",", urlPositions);
        }
    }
}
