using Api.Business.Event.Handler;
using Api.Cores.Base.Events.Model;
using Api.Cores.Base.EventStores.Repository;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Cores.Base.Events.Bus
{
    public class EventBus : IEventBus
    {
        private readonly IEventStoreRepository eventStoreRepository;
        private readonly ConcurrentDictionary<IEvent, IEventHandler> keyValuePairs = null;

        public EventBus(IEventStoreRepository eventStoreRepository)
        {
            this.eventStoreRepository = eventStoreRepository;
            keyValuePairs = new ConcurrentDictionary<IEvent, IEventHandler>();
        }

        Task IEventBus.BroadcastEventsAsync()
        {
            try
            {
                //var eventHandler = keyValuePair.Value;

                //eventHandler.HandleAsync(eventStoreRepository, keyValuePair.Key);

                keyValuePairs
                  .ToList()
                  .ForEach(async (keyValuePair) =>
                  {
                      try
                      {
                          //Type keyType = keyValuePair.Key.GetType(); // IEvent
                          //Type valueType = typeof(IEventHandler<>); // EventHandler

                          //var genericType = valueType.MakeGenericType(keyType); // Dynamically add Generic Type
                          //var getmethod = genericType.GetMethod("HandleAsync"); // Get Method from Interface by specified method name.

                          //getmethod.Invoke(keyValuePair.Value, new object[] { eventStoreRepository, keyValuePair.Key }); // Invoke method dynamically. First Parameter : EventHandler Instance, Second Parameter : event value

                          await keyValuePair.Value.HandleAsync(eventStoreRepository, keyValuePair.Key);
                      }
                      catch
                      {
                          throw;
                      }
                  });
            }
            catch
            {
                throw;
            }

            return Task.CompletedTask;
        }

        IEventBus IEventBus.RegisterEvent(IEvent @event, IEventHandler eventHandler)
        {
            try
            {
                keyValuePairs.GetOrAdd(key: @event, value: eventHandler);
                return this;
            }
            catch
            {
                throw;
            }
        }
    }
}