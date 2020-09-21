using Api.Cores.Repository.Reads;
using Api.Cores.SqlDbProviders;
using Api.Infrastructures.Repository.Abstracts;
using Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Infrastructures.Repository.Reads
{
    public sealed class GetAllMovieRepository : MovieBaseRepositoryAbstract, IGetAllMovieRepository
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public GetAllMovieRepository(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        async Task<IReadOnlyCollection<MovieModel>> IGetAllMovieRepository.GetAllMovieAsync()
        {
            try
            {
                return
                    await
                        sqlClientDbProvider
                        ?.DapperBuilder
                        ?.OpenConnection(sqlClientDbProvider.GetConnection())
                        ?.Parameter(async () => await base.GetParameterAsync("All-Movie-Data"))
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