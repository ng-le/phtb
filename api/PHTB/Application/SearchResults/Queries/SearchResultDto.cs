using Domain.Enums;

namespace Application.SearchResults.Queries
{
    public class SearchResultDto
    {
        public SearchEngineType SearchEngineType { get; set; }
        public string? Keywords { get; set; }
        public string? TargetUrl { get; set; }
        public string? Result { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
