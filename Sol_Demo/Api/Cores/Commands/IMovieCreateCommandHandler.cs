using Api.Business.Command.Commands;
using Api.Cores.Base.Commands.Handler;

namespace Api.Cores.Commands
{
    public interface IMovieCreateCommandHandler : ICommandHandler<MovieCreateCommand, object>
    {
    }
}