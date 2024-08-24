using Application.Common.Interfaces;
using Application.SearchResults.Queries;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.SearchResults.Commands
{
    public record CreateSearchResultCommand(SearchResultDto SearchResult) : IRequest<Guid>;

    public class CreateSearchResultHandler : IRequestHandler<CreateSearchResultCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateSearchResultHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(CreateSearchResultCommand request, CancellationToken cancellationToken)
        {
            var searchResult = _mapper.Map<SearchResult>(request.SearchResult);
            searchResult.Id = Guid.NewGuid();

            _context.SearchResults.Add(searchResult);
            await _context.SaveChangesAsync(cancellationToken);

            return searchResult.Id;
        }
    }
}
