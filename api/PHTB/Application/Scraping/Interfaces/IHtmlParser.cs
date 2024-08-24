using Domain.Enums;

namespace Application.Scraping.Interfaces
{
    public interface IHtmlParser
    {
        /// <summary>
        /// Parse html content and extract urls
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        List<string> ParseUrls(string htmlContent, SearchEngineType searchEngineType);
    }
}
