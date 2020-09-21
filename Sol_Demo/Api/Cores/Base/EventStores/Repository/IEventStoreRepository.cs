using Api.Cores.Base.EventStores.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Cores.Base.EventStores.Repository
{
    public interface IAppendEventStore
    {
        Task AppendAsync(EventModel eventModel);
    }

    public interface IReadEventStore
    {
        Task<IEnumerable<EventModel>> ReadByAggregateAsync(Guid aggregateId);

        Task<IEnumerable<EventModel>> ReadByStateAsync(Guid stateId);
    }

    public interface IEventStoreRepository : IAppendEventStore, IReadEventStore
    {
    }
}