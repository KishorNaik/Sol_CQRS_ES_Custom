using Api.Cores.Repository.Reads;
using Api.Cores.SqlDbProviders;
using Api.Infrastructures.Repository.Abstracts;
using Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Infrastructures.Repository.Reads
{
    public sealed class GetMovieByReleaseDateRepository : MovieBaseRepositoryAbstract, IGetMovieByReleaseDateRepository
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public GetMovieByReleaseDateRepository(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        async Task<IReadOnlyCollection<MovieModel>> IGetMovieByReleaseDateRepository.GetMovieByReleaseDateAsync(MovieModel movieModel)
        {
            try
            {
                return
                    await
                        sqlClientDbProvider
                        ?.DapperBuilder
                        ?.OpenConnection(sqlClientDbProvider.GetConnection())
                        ?.Parameter(async () => await base.GetParameterAsync("Movie-Release-Date", movieModel))
                        ?.Command(async (dbConnection, dynamicParameter) => await base.ExecuteQueryAsync(dbConnection, dynamicParameter))
                        ?.ResultAsync<IReadOnlyCollection<MovieModel>>();
            }
            catch
            {
                throw;
            }
        }
    }
}