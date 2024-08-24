using Application.Scraping.Interfaces;
using Domain.Enums;
using System.Text.RegularExpressions;

namespace Infrastructure.Services.Scraping
{
    public class HtmlParser : IHtmlParser
    {
        public List<string> ParseUrls(string htmlContent, SearchEngineType searchEngineType)
        {
            var urls = new List<string>();
            string pattern = searchEngineType switch
            {
                SearchEngineType.Google => @"<a href=""/url\?q=(?<url>https?://[^&]*)",
                SearchEngineType.Bing => @"<a href=""(?<url>https?://[^""]*)""",
                _ => throw new ArgumentException("Unsupported search engine type")
            };

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(htmlContent);

            foreach (Match match in matches)
            {
                if (match.Groups["url"].Success)
                {
                    string url = match.Groups["url"].Value;
                    urls.Add(Uri.UnescapeDataString(url));
                }
            }

            return urls;
        }
    }
}
