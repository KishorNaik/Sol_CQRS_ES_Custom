using Api.Business.Query.Queries;
using Api.Cores.Api.Queries;
using Api.Cores.Base.Api.Query;
using Api.Cores.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Applications.ApiQueries
{
    public sealed class GetMovieByTitleApiQueryHandler : IGetMovieByTitleApiQueryHandler
    {
        private readonly IGetMovieByTitleQueryHandler getMovieByTitleQueryHandler = null;

        public GetMovieByTitleApiQueryHandler(IGetMovieByTitleQueryHandler getMovieByTitleQueryHandler)
        {
            this.getMovieByTitleQueryHandler = getMovieByTitleQueryHandler;
        }

        async Task<IActionResult> IApiQueryHandler<GetMovieByTitleQuery>.HandleAsync(ControllerBase controllerBase, GetMovieByTitleQuery query)
        {
            try
            {
                if (query == null) return controllerBase?.BadRequest();

                return controllerBase?.Ok(await getMovieByTitleQueryHandler?.HandleAsync(query));
            }
            catch
            {
                throw;
            }
        }
    }
}