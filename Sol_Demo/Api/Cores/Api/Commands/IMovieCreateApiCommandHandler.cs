using Api.Business.Command.Commands;
using Api.Cores.Base.Api.Command;

namespace Api.Cores.Api.Commands
{
    public interface IMovieCreateApiCommandHandler : IApiCommandHandler<MovieCreateCommand>
    {
    }
}