using Api.Business.Command.Commands;
using Api.Models;
using AutoMapper;

namespace Api.Mappers.Commands
{
    public class MovieCreateCommandMapperProfile : Profile
    {
        public MovieCreateCommandMapperProfile()
        {
            base.CreateMap<MovieCreateCommand, MovieModel>();
        }
    }
}