using Api.Business.Query.Queries;
using Api.Cores.Api.Queries;
using Api.Cores.Base.Api.Query;
using Api.Cores.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Applications.ApiQueries
{
    public sealed class GetMovieByReleaseDateApiQueryHandler : IGetMovieByReleaseDateApiQueryHandler
    {
        private readonly IGetMovieByReleaseDateQueryHandler getMovieByReleaseDateQueryHandler = null;

        public GetMovieByReleaseDateApiQueryHandler(IGetMovieByReleaseDateQueryHandler getMovieByReleaseDateQueryHandler)
        {
            this.getMovieByReleaseDateQueryHandler = getMovieByReleaseDateQueryHandler;
        }

        async Task<IActionResult> IApiQueryHandler<GetMovieByReleaseDateQuery>.HandleAsync(ControllerBase controllerBase, GetMovieByReleaseDateQuery query)
        {
            try
            {
                if (query == null) return controllerBase.BadRequest();

                return controllerBase.Ok(await getMovieByReleaseDateQueryHandler?.HandleAsync(query));
            }
            catch
            {
                throw;
            }
        }
    }
}