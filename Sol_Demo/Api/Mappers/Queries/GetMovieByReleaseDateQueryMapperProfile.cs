using Api.Business.Query.Queries;
using Api.Models;
using AutoMapper;

namespace Api.Mappers.Queries
{
    public sealed class GetMovieByReleaseDateQueryMapperProfile : Profile
    {
        public GetMovieByReleaseDateQueryMapperProfile()
        {
            base.CreateMap<GetMovieByReleaseDateQuery, MovieModel>();
        }
    }
}