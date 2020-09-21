using Api.Business.Command.Commands;
using Api.Models;
using AutoMapper;

namespace Api.Mappers.Commands
{
    public sealed class MovieUpdateCommandMapperProfile : Profile
    {
        public MovieUpdateCommandMapperProfile()
        {
            base.CreateMap<MovieUpdateCommand, MovieModel>();
        }
    }
}