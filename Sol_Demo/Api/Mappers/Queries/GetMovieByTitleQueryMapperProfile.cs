using Api.Business.Query.Queries;
using Api.Models;
using AutoMapper;

namespace Api.Mappers.Queries
{
    public sealed class GetMovieByTitleQueryMapperProfile : Profile
    {
        public GetMovieByTitleQueryMapperProfile()
        {
            base.CreateMap<GetMovieByTitleQuery, MovieModel>();
        }
    }
}