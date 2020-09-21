using Api.Cores.Repository.Writes;
using Api.Cores.SqlDbProviders;
using Api.Infrastructures.Repository.Abstracts;
using Api.Models;
using Dapper;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Api.Infrastructures.Repository.Writes
{
    public sealed class MovieCreateRepository : MovieBaseRepositoryAbstract, IMovieCreateRepository
    {
        public ISqlClientDbProvider sqlClientDbProvider = null;

        public MovieCreateRepository(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        public event EventHandler<MovieModel> DataEventStoreHandler;

        private async Task<bool> ExecuteCommandAsync(MovieModel movieModel, IDbConnection dbConnection, DynamicParameters dynamicParameter)
        {
            try
            {
                var grider =
                        (await
                            dbConnection
                            .QueryMultipleAsync("uspSetMovie", param: dynamicParameter, commandType: CommandType.StoredProcedure)
                        );

                var message = (await grider.ReadFirstOrDefaultAsync<MessageModel>()).Message;

                if (message.Contains("Add"))
                {
                    var movieIdentity = (await grider.ReadFirstOrDefaultAsync<MovieModel>()).MovieIdentity;
                    movieModel.MovieIdentity = movieIdentity;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }

        async Task<bool?> IMovieCreateRepository.CreateAsync(MovieModel movieModel)
        {
            try
            {
                var flag =
                    await
                        sqlClientDbProvider
                        ?.DapperBuilder
                        ?.OpenConnection(sqlClientDbProvider.GetConnection())
                        ?.Parameter(async () => await base.SetParameterAsync("Add", movieModel))
                        ?.Command(async (dbConnection, dynamicParameter) => await this.ExecuteCommandAsync(movieModel, dbConnection, dynamicParameter))
                        ?.ResultAsync<bool?>();

                if (flag == true) DataEventStoreHandler?.Invoke(this, movieModel);

                return flag;
            }
            catch
            {
                throw;
            }
        }
    }
}