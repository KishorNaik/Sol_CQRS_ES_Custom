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
    public sealed class GetMovieByReleaseDateQueryHandler : MovieBaseQueryAbstract, IGetMovieByReleaseDateQueryHandler
    {
        private readonly IGetMovieByReleaseDateRepository getMovieByReleaseDateRepository = null;
        private readonly IMapper mapper = null;

        public GetMovieByReleaseDateQueryHandler(IGetMovieByReleaseDateRepository getMovieByReleaseDateRepository, IMapper mapper)
        {
            this.getMovieByReleaseDateRepository = getMovieByReleaseDateRepository;
            this.mapper = mapper;
        }

        async Task<object> IQueryHandler<GetMovieByReleaseDateQuery, object>.HandleAsync(GetMovieByReleaseDateQuery query)
        {
            try
            {
                var repositoryResponse = await getMovieByReleaseDateRepository?.GetMovieByReleaseDateAsync(this.mapper.Map<MovieModel>(query));

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