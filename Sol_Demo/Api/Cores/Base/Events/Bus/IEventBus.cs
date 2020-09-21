using Api.Business.Event.Handler;
using Api.Cores.Base.Events.Model;
using System.Threading.Tasks;

namespace Api.Cores.Base.Events
{
    public interface IEventBus
    {
        IEventBus RegisterEvent(IEvent @event, IEventHandler eventHandler);

        Task BroadcastEventsAsync();
    }
}