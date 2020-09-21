using Api.Cores.Api.Queries;
using Api.Cores.Base.Api.Query;
using Api.Cores.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Applications.Api.Queries
{
    public sealed class GetAllMovieApiCommandHandler : IGetAllMovieApiQueryHandler
    {
        private readonly IGetAllMovieQueryHandler getAllMovieQueryHandler = null;

        public GetAllMovieApiCommandHandler(IGetAllMovieQueryHandler getAllMovieQueryHandler)
        {
            this.getAllMovieQueryHandler = getAllMovieQueryHandler;
        }

        async Task<IActionResult> IApiQueryHandler.HandleAsync(ControllerBase controllerBase)
        {
            try
            {
                return controllerBase.Ok(await getAllMovieQueryHandler.HandleAsync());
            }
            catch
            {
                throw;
            }
        }
    }
}