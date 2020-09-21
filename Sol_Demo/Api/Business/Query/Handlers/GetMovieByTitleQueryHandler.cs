using Api.Business.Query.Abstracts;
using Api.Business.Query.Queries;
using Api.Cores.Base.Query.Handler;
using Api.Cores.Queries;
using Api.Cores.Repository.Reads;
using Api.Models;
using AutoMapper;
using System.Threading.Tasks;

namespace Api.Business.Query.Handlers
{
    public sealed class GetMovieByTitleQueryHandler : MovieBaseQueryAbstract, IGetMovieByTitleQueryHandler
    {
        private readonly IGetMovieByTitleRepository getMovieByTitleRepository = null;
        private readonly IMapper mapper = null;

        public GetMovieByTitleQueryHandler(IGetMovieByTitleRepository getMovieByTitleRepository, IMapper mapper)
        {
            this.getMovieByTitleRepository = getMovieByTitleRepository;
            this.mapper = mapper;
        }

        async Task<object> IQueryHandler<GetMovieByTitleQuery, object>.HandleAsync(GetMovieByTitleQuery query)
        {
            try
            {
                var repositoryResponse = await getMovieByTitleRepository?.GetMovieByTitleAsync(this.mapper.Map<MovieModel>(query));

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