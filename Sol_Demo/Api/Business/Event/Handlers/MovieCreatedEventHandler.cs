using Api.Business.Event.Handler;
using Api.Cores.Base.Events.Model;
using Api.Cores.Base.EventStores.Model;
using Api.Cores.Base.EventStores.Repository;
using Api.Cores.Events;
using System.Threading.Tasks;

namespace Api.Business.Event.Handlers
{
    public sealed class MovieCreatedEventHandler : IMovieCreatedEventHandler
    {
        async Task IEventHandler.HandleAsync(IEventStoreRepository eventStoreRepository, IEvent @event)
        {
            try
            {
                await eventStoreRepository?.AppendAsync(@event as EventModel);
            }
            catch
            {
                throw;
            }
        }
    }
}