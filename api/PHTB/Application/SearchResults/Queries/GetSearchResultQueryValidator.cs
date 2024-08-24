using FluentValidation;

namespace Application.SearchResults.Queries
{
    public class GetSearchResultQueryValidator : AbstractValidator<GetSearchResultQuery>
    {
        public GetSearchResultQueryValidator() 
        {
            RuleFor(v => v.Keywords).MaximumLength(512).NotEmpty();
            RuleFor(v => v.TargetUrl).MaximumLength(512).NotEmpty();
        }
    }
}
