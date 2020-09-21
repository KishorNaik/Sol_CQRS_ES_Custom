using Api.Cores.Base.EventStores.Model;
using Api.Cores.Base.EventStores.Repository;
using Api.Cores.SqlDbProviders;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Api.Infrastructures.EventStoreRepository
{
    public sealed class EventStoreRepository : IEventStoreRepository
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public EventStoreRepository(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        async Task IAppendEventStore.AppendAsync(EventModel eventModel)
        {
            try
            {
                await
                    sqlClientDbProvider
                    ?.DapperBuilder
                    ?.OpenConnection(sqlClientDbProvider.GetConnection())
                    ?.Parameter(() =>
                    {
                        var dynamicParameters = new DynamicParameters();
                        dynamicParameters.Add("@AggregateId", eventModel?.AggregateId, DbType.Guid, ParameterDirection.Input);
                        dynamicParameters.Add("@StateId", eventModel?.StateId, DbType.Guid, ParameterDirection.Input);
                        dynamicParameters.Add("@EventName", eventModel?.EventName, DbType.String, ParameterDirection.Input);
                        dynamicParameters.Add("@NewPayLoad", eventModel?.NewPayLoad, DbType.String, ParameterDirection.Input);
                        dynamicParameters.Add("@OldPayLoad", eventModel?.OldPayLoad, DbType.String, ParameterDirection.Input);
                        dynamicParameters.Add("@CreatedDate", eventModel?.CreatedDate, DbType.DateTime, ParameterDirection.Input);
                        return dynamicParameters;
                    })
                    ?.Command(async (dbConnection, dynamicParameter) =>
                    {
                        string command =
                            new StringBuilder()
                            .Append("INSERT INTO dbo.EventStore ")
                            .Append("(AggregateId,StateId,EventName,NewPayLoad,OldPayLoad,CreatedDate) ")
                            .Append("VALUES ")
                            .Append("(@AggregateId,@StateId,@EventName,@NewPayLoad,@OldPayLoad,@CreatedDate)")
                            .ToString();

                        var status = await dbConnection?.ExecuteAsync(sql: command, param: dynamicParameter, commandType: CommandType.Text);

                        return status;
                    })
                    ?.ResultAsync<int?>();
            }
            catch
            {
                throw;
            }
        }

        async Task<IEnumerable<EventModel>> IReadEventStore.ReadByAggregateAsync(Guid aggregateId)
        {
            try
            {
                return
                    await
                     sqlClientDbProvider
                     .DapperBuilder
                    .OpenConnection(sqlClientDbProvider.GetConnection())
                    .Parameter(() =>
                    {
                        var dynamicParameters = new DynamicParameters();
                        dynamicParameters.Add("@AggregateId", aggregateId, DbType.Guid, ParameterDirection.Input);

                        return dynamicParameters;
                    })
                    .Command(async (dbConnection, dynamicParameter) =>
                    {
                        string command = "SELECT * FROM EventStore WHERE AggregateId=@AggregateId ORDER BY EventId DESC";

                        var data = await dbConnection?.QueryAsync<EventModel>(sql: command, param: dynamicParameter, commandType: CommandType.Text);
                        return data;
                    })
                    .ResultAsync<IEnumerable<EventModel>>();
            }
            catch
            {
                throw;
            }
        }

        async Task<IEnumerable<EventModel>> IReadEventStore.ReadByStateAsync(Guid stateId)
        {
            try
            {
                return
                    await
                    sqlClientDbProvider
                    .DapperBuilder
                    .OpenConnection(sqlClientDbProvider.GetConnection())
                    .Parameter(() =>
                    {
                        var dynamicParameters = new DynamicParameters();
                        dynamicParameters.Add("@StateId", stateId, DbType.Guid, ParameterDirection.Input);

                        return dynamicParameters;
                    })
                    .Command(async (dbConnection, dynamicParameter) =>
                    {
                        string command = "SELECT * FROM EventStore WHERE StateId=@stateId ORDER BY EventId DESC";

                        var data = await dbConnection?.QueryAsync<EventModel>(sql: command, param: dynamicParameter, commandType: CommandType.Text);
                        return data;
                    })
                    .ResultAsync<IEnumerable<EventModel>>();
            }
            catch
            {
                throw;
            }
        }
    }
}