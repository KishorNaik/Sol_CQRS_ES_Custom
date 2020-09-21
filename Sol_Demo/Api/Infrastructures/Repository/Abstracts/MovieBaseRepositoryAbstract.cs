using Api.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Infrastructures.Repository.Abstracts
{
    public abstract class MovieBaseRepositoryAbstract
    {
        protected virtual Task<DynamicParameters> SetParameterAsync(String paraCommand, MovieModel movieModel = null)
        {
            try
            {
                return Task.Run<DynamicParameters>(() =>
                {
                    var dynamicParameterObj = new DynamicParameters();
                    dynamicParameterObj.Add("@Command", paraCommand, DbType.String, ParameterDirection.Input);
                    dynamicParameterObj.Add("@AggregateId", movieModel?.AggregateId, DbType.Guid, ParameterDirection.Input);
                    dynamicParameterObj.Add("@MovieIdentity", movieModel?.MovieIdentity, DbType.Guid, ParameterDirection.Input);
                    dynamicParameterObj.Add("@Title", movieModel?.Title, DbType.String, ParameterDirection.Input);
                    dynamicParameterObj.Add("@ReleaseDate", movieModel?.ReleaseDate, DbType.DateTime, ParameterDirection.Input);
                    dynamicParameterObj.Add("@IsDelete", movieModel?.IsDelete, DbType.Boolean, ParameterDirection.Input);

                    return dynamicParameterObj;
                });
            }
            catch
            {
                throw;
            }
        }

        protected virtual Task<DynamicParameters> GetParameterAsync(String paraCommand, MovieModel movieModel = null)
        {
            try
            {
                return Task.Run<DynamicParameters>(() =>
                {
                    var dynamicParameterObj = new DynamicParameters();
                    dynamicParameterObj.Add("@Command", paraCommand, DbType.String, ParameterDirection.Input);
                    dynamicParameterObj.Add("@Title", movieModel?.Title, DbType.String, ParameterDirection.Input);
                    dynamicParameterObj.Add("@ReleaseStartDate", movieModel?.ReleaseStartDate, DbType.DateTime, ParameterDirection.Input);
                    dynamicParameterObj.Add("@ReleaseEndDate", movieModel?.ReleaseEndDate, DbType.DateTime, ParameterDirection.Input);

                    return dynamicParameterObj;
                });
            }
            catch
            {
                throw;
            }
        }

        protected async Task<IReadOnlyCollection<MovieModel>> ExecuteQueryAsync(IDbConnection dbConnection, DynamicParameters dynamicParameters)
        {
            try
            {
                return
                    (await
                    dbConnection
                    ?.QueryAsync<MovieModel>(sql: "uspGetMovie", param: dynamicParameters, commandType: CommandType.StoredProcedure)
                    )
                    .ToList()
                    .AsReadOnly();
            }
            catch
            {
                throw;
            }
        }
    }
}