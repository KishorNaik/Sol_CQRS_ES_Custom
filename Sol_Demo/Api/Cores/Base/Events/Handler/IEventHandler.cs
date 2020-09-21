using Api.Cores.Base.Events.Model;
using Api.Cores.Base.EventStores.Repository;
using System.Threading.Tasks;

namespace Api.Business.Event.Handler
{
    public interface IEventHandler
    {
        Task HandleAsync(IEventStoreRepository eventStoreRepository, IEvent @event);
    }
}