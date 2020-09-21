using Api.Business.Command.Commands;
using Api.Cores.Api.Commands;
using Api.Cores.Base.Api.Command;
using Api.Cores.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Applications.ApiCommands
{
    public sealed class MovieDeleteApiCommandHandler : IMovieDeleteApiCommandHandler
    {
        private readonly IMovieDeleteCommandHandler movieDeleteCommandHandler = null;

        public MovieDeleteApiCommandHandler(IMovieDeleteCommandHandler movieDeleteCommandHandler)
        {
            this.movieDeleteCommandHandler = movieDeleteCommandHandler;
        }

        async Task<IActionResult> IApiCommandHandler<MovieDeleteCommand>.HandleAsync(ControllerBase controllerBase, MovieDeleteCommand command)
        {
            try
            {
                if (command == null) return controllerBase?.BadRequest();
                return controllerBase?.Ok(await movieDeleteCommandHandler?.HandleAsync(command));
            }
            catch
            {
                throw;
            }
        }
    }
}