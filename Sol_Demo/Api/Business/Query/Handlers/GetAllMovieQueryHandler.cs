using Api.Business.Query.Abstracts;
using Api.Cores.Base.Query.Handler;
using Api.Cores.Queries;
using Api.Cores.Repository.Reads;
using System.Threading.Tasks;

namespace Api.Business.Query.Handlers
{
    public sealed class GetAllMovieQueryHandler : MovieBaseQueryAbstract, IGetAllMovieQueryHandler
    {
        private readonly IGetAllMovieRepository getAllMovieRepository = null;

        public GetAllMovieQueryHandler(IGetAllMovieRepository getAllMovieRepository)
        {
            this.getAllMovieRepository = getAllMovieRepository;
        }

        async Task<object> IQueryHandler<object>.HandleAsync()
        {
            try
            {
                var repositoryResponse = await getAllMovieRepository?.GetAllMovieAsync();

                if (repositoryResponse?.Count == 0 || repositoryResponse == null) return await base.NotFoundAsync();

                return repositoryResponse;
            }
            catch
            {
                throw;
            }
        }
    }
}