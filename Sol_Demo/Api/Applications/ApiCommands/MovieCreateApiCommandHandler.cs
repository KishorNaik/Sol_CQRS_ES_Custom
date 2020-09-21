using Api.Business.Command.Commands;
using Api.Cores.Api.Commands;
using Api.Cores.Base.Api.Command;
using Api.Cores.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Applications.ApiCommands
{
    public sealed class MovieCreateApiCommandHandler : IMovieCreateApiCommandHandler
    {
        private readonly IMovieCreateCommandHandler movieCreateCommandHandler = null;

        public MovieCreateApiCommandHandler(IMovieCreateCommandHandler movieCreateCommandHandler)
        {
            this.movieCreateCommandHandler = movieCreateCommandHandler;
        }

        async Task<IActionResult> IApiCommandHandler<MovieCreateCommand>.HandleAsync(ControllerBase controllerBase, MovieCreateCommand command)
        {
            try
            {
                if (command == null) return controllerBase.BadRequest();

                return controllerBase.Ok(await movieCreateCommandHandler?.HandleAsync(command));
            }
            catch
            {
                throw;
            }
        }
    }
}