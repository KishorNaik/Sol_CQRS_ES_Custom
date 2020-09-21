using Api.Cores.Repository.Writes;
using Api.Cores.SqlDbProviders;
using Api.Infrastructures.Repository.Abstracts;
using Api.Models;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Infrastructures.Repository.Writes
{
    public sealed class MovieDeleteRepository : MovieBaseRepositoryAbstract, IMovieDeleteRepository
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public MovieDeleteRepository(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        public event EventHandler<MovieModel> DataEventStoreHandler;

        private async Task<(bool? flag, MovieModel deleteMovieModel)> ExecuteCommandAsync(MovieModel movieModel, IDbConnection dbConnection, DynamicParameters dynamicParameters)
        {
            var tuplesObj = (
                                       flag: default(bool?),
                                       deleteMovieModel: default(MovieModel)
                                    );
            try
            {
                var data =
                        await
                            dbConnection
                            ?.QueryAsync<MovieModel>(sql: "uspSetMovie", param: dynamicParameters, commandType: CommandType.StoredProcedure);

                if (data?.Count() >= 1) tuplesObj.flag = true;

                tuplesObj.deleteMovieModel = data.FirstOrDefault();

                return tuplesObj;
            }
            catch
            {
                throw;
            }
        }

        async Task<bool?> IMovieDeleteRepository.DeleteAsync(MovieModel movieModel)
        {
            MovieModel deleteMovieModel = default(MovieModel);
            try
            {
                var flag =
                        await
                        sqlClientDbProvider
                        ?.DapperBuilder
                        ?.OpenConnection(sqlClientDbProvider.GetConnection())
                        ?.Parameter(async () => await base.SetParameterAsync("Delete", movieModel))
                        ?.Command(async (dbConnection, dynamicParameters) =>
                        {
                            (bool? flag, MovieModel deleteMovieModel) tuplesObj = await this.ExecuteCommandAsync(movieModel, dbConnection, dynamicParameters);

                            deleteMovieModel = tuplesObj.deleteMovieModel;
                            deleteMovieModel.StateId = movieModel.StateId;

                            return tuplesObj.flag;
                        })
                        ?.ResultAsync<bool?>();

                if (flag == true) this.DataEventStoreHandler?.Invoke(this, deleteMovieModel);

                return flag;
            }
            catch
            {
                throw;
            }
        }
    }
}