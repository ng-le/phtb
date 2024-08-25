using Api.Filters;
using Application.SearchResults.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [ApiExceptionFilter]
    [Route("api/[controller]")]
    public class SearchResultController : ControllerBase
    {
        private readonly ISender _mediator;
        public SearchResultController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<SearchResultDto>> GetSearchResult([FromQuery] GetSearchResultQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("all")]
        public async Task<List<SearchResultDto>> GetAllSearchResults()
        {
            return await _mediator.Send(new GetAllSearchResultsQuery());
        }
    }
}
