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
    public sealed class MovieUpdateRepository : MovieBaseRepositoryAbstract, IMovieUpdateRepository
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public MovieUpdateRepository(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        public event Action<object, MovieModel, MovieModel> DataEventStoreHandler;

        private async Task<(bool flag, MovieModel oldMovieModel)> ExecuteCommandAsync(MovieModel newModel, IDbConnection dbConnection, DynamicParameters dynamicParameters)
        {
            MovieModel tempOldMovieModel = null;
            bool tempFlag = false;
            try
            {
                // Execute Command
                var grider =
                            await
                            dbConnection
                            ?.QueryMultipleAsync(sql: "uspSetMovie", param: dynamicParameters, commandType: CommandType.StoredProcedure);

                // First Grider
                var message = (await grider.ReadFirstOrDefaultAsync<MessageModel>()).Message;

                if (message.Contains("Update"))
                {
                    // Second Grider (Old Data)
                    tempOldMovieModel = await grider.ReadFirstOrDefaultAsync<MovieModel>();
                    tempFlag = true;
                }

                var tupleObj = (
                        flag: tempFlag,
                        oldMovieModel: tempOldMovieModel
                    );

                return tupleObj;
            }
            catch
            {
                throw;
            }
        }

        async Task<bool?> IMovieUpdateRepository.UpdateAsync(MovieModel movieModel)
        {
            MovieModel oldMovieModel = null;
            try
            {
                var flag =
                        await
                            sqlClientDbProvider
                            ?.DapperBuilder
                            ?.OpenConnection(sqlClientDbProvider.GetConnection())
                            ?.Parameter(async () => await base.SetParameterAsync("Update", movieModel))
                            ?.Command(async (dbConnection, dynamicParameter) =>
                            {
                                (bool flag, MovieModel oldMovieModel) tupleObj = await this.ExecuteCommandAsync(movieModel, dbConnection, dynamicParameter);
                                oldMovieModel = tupleObj.oldMovieModel;
                                return tupleObj.flag;
                            })
                            ?.ResultAsync<bool?>();

                if (flag == true) DataEventStoreHandler?.Invoke(this, oldMovieModel, movieModel);

                return flag;
            }
            catch
            {
                throw;
            }
        }
    }
}