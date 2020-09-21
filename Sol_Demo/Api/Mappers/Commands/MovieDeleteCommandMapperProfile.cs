using Api.Business.Command.Commands;
using Api.Models;
using AutoMapper;

namespace Api.Mappers.Commands
{
    public class MovieDeleteCommandMapperProfile : Profile
    {
        public MovieDeleteCommandMapperProfile()
        {
            base.CreateMap<MovieDeleteCommand, MovieModel>();
        }
    }
}