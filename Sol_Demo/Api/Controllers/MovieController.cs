using Api.Business.Command.Commands;
using Api.Business.Query.Queries;
using Api.Cores.Api.Commands;
using Api.Cores.Api.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        [HttpPost("addmovie")]
        public Task<IActionResult> CreateMovieAsync(
            [FromBody] MovieCreateCommand command,
            [FromServices] IMovieCreateApiCommandHandler movieCreateApiCommandHandler
            ) => movieCreateApiCommandHandler.HandleAsync(this, command);

        [HttpPost("updatemovie")]
        public Task<IActionResult> UpdateMovieAsync(
            [FromBody] MovieUpdateCommand command,
            [FromServices] IMovieUpdateApiCommandHandler movieUpdateApiCommandHandler
            ) => movieUpdateApiCommandHandler.HandleAsync(this, command);

        [HttpPost("deletemovie")]
        public Task<IActionResult> DeleteMovieAsync
            (
                [FromBody] MovieDeleteCommand movieDeleteCommand,
                [FromServices] IMovieDeleteApiCommandHandler movieDeleteApiCommandHandler
            ) => movieDeleteApiCommandHandler.HandleAsync(this, movieDeleteCommand);

        [HttpPost("getallmovie")]
        public Task<IActionResult> GetAllMovieAsync([FromServices] IGetAllMovieApiQueryHandler getAllMovieApiQueryHandler) => getAllMovieApiQueryHandler?.HandleAsync(this);

        [HttpPost("getmoviebytitles")]
        public Task<IActionResult> GetMovieByTitleAsync
            (
                [FromBody] GetMovieByTitleQuery query,
                [FromServices] IGetMovieByTitleApiQueryHandler getMovieByTitleApiQueryHandler
            ) => getMovieByTitleApiQueryHandler?.HandleAsync(this, query);

        [HttpPost("getmoviebyreleasedate")]
        public Task<IActionResult> GetMovieByReleaseDateAsync
            (
                [FromBody] GetMovieByReleaseDateQuery query,
                [FromServices] IGetMovieByReleaseDateApiQueryHandler getMovieByReleaseDateApiQueryHandler
            ) => getMovieByReleaseDateApiQueryHandler?.HandleAsync(this, query);
    }
}