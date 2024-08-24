using Application.Scraping.Interfaces;
using Application.SearchResults.Commands;
using Domain.Enums;
using MediatR;

namespace Application.SearchResults.Queries
{
    public record GetSearchResultQuery(string Keywords, string TargetUrl, SearchEngineType SearchEngineType) : IRequest<SearchResultDto>;

    public class GetSearchResultQueryHandler : IRequestHandler<GetSearchResultQuery, SearchResultDto>
    {
        private readonly ISearchServiceFactory _searchServiceFactory;
        private readonly ISender _mediator;
        public GetSearchResultQueryHandler(ISearchServiceFactory searchServiceFactory, ISender mediator)
        {
            _searchServiceFactory = searchServiceFactory;
            _mediator = mediator;
        }
        public async Task<SearchResultDto> Handle(GetSearchResultQuery request, CancellationToken cancellationToken)
        {
            var searchService = _searchServiceFactory.Create(request.SearchEngineType);
            var urlPositions = await searchService.GetUrlPositions(request.Keywords, request.TargetUrl);
            var result = new SearchResultDto
            {
                SearchEngineType = request.SearchEngineType,
                Keywords = request.Keywords,
                TargetUrl = request.TargetUrl,
                Result = urlPositions,
                CreatedOn = DateTime.UtcNow
            };

            await _mediator.Send(new CreateSearchResultCommand(result));

            return result;
        }
    }
}
