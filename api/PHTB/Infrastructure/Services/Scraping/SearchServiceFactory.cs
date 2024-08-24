using Application.Scraping.Interfaces;
using Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Scraping
{
    public class SearchServiceFactory : ISearchServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SearchServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ISearchService Create(SearchEngineType searchEngineType)
        {
            return searchEngineType switch
            {
                SearchEngineType.Google => _serviceProvider.GetRequiredService<GoogleSearchService>(),
                SearchEngineType.Bing => _serviceProvider.GetRequiredService<BingSearchService>(),
                _ => throw new ArgumentException("Invalid search engine type")
            };
        }
    }
}
