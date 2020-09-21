using Api.Business.Command.Commands;
using Api.Cores.Api.Commands;
using Api.Cores.Base.Api.Command;
using Api.Cores.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Applications.ApiCommands
{
    public sealed class MovieUpdateApiCommandHandler : IMovieUpdateApiCommandHandler
    {
        private readonly IMovieUpdateCommandHandler movieUpdateCommandHandler = null;

        public MovieUpdateApiCommandHandler(IMovieUpdateCommandHandler movieUpdateCommandHandler)
        {
            this.movieUpdateCommandHandler = movieUpdateCommandHandler;
        }

        async Task<IActionResult> IApiCommandHandler<MovieUpdateCommand>.HandleAsync(ControllerBase controllerBase, MovieUpdateCommand command)
        {
            try
            {
                if (command == null) return controllerBase?.BadRequest();
                return controllerBase?.Ok(await movieUpdateCommandHandler?.HandleAsync(command));
            }
            catch
            {
                throw;
            }
        }
    }
}