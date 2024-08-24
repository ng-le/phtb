using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.SearchResults.Queries
{
    public record GetAllSearchResultsQuery : IRequest<List<SearchResultDto>>;

    public class GetAllSearchResultsQueryHandler : IRequestHandler<GetAllSearchResultsQuery, List<SearchResultDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllSearchResultsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<SearchResultDto>> Handle(GetAllSearchResultsQuery request, CancellationToken cancellationToken)
        {
            var searchResults = await _context.SearchResults.AsNoTracking().OrderByDescending(s => s.CreatedOn).ToListAsync();
            return _mapper.Map<List<SearchResultDto>>(searchResults);
        }
    }
}
