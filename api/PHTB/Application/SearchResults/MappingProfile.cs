using Application.SearchResults.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.SearchResults
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<SearchResult, SearchResultDto>();
            CreateMap<SearchResultDto, SearchResult>();
        }
    }
}
