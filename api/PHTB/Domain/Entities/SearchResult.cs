using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class SearchResult : BaseEntity
    {
        public SearchEngineType SearchEngineType { get; set; }
        public string? Keywords { get; set; }
        public string? TargetUrl { get; set; }
        public string? Result { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
