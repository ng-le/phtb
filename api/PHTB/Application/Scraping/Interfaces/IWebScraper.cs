namespace Application.Scraping.Interfaces
{
    public interface IWebScraper
    {
        /// <summary>
        /// Get the HTML content from a given URL
        /// </summary>
        Task<string> GetPageContent(string url);
    }
}
