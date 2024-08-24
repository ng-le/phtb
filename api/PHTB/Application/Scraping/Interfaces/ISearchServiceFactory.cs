using Domain.Enums;

namespace Application.Scraping.Interfaces
{
    public interface ISearchServiceFactory
    {
        ISearchService Create(SearchEngineType searchEngineType);
    }
}
