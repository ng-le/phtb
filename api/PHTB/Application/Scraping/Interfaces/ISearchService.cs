namespace Application.Scraping.Interfaces
{
    public interface ISearchService
    {
        /// <summary>
        /// Search for a a URL's position using specific keywords
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="targetUrl"></param>
        /// <returns></returns>
        Task<string> GetUrlPositions(string keywords, string targetUrl);
    }
}
