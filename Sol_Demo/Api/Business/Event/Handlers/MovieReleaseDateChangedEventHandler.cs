using Api.Business.Event.Handler;
using Api.Cores.Base.Events.Model;
using Api.Cores.Base.EventStores.Model;
using Api.Cores.Base.EventStores.Repository;
using Api.Cores.Events;
using Api.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Api.Business.Event.Handlers
{
    public sealed class MovieReleaseDateChangedEventHandler : IMovieReleaseDateChangedEventHandler
    {
        async Task IEventHandler.HandleAsync(IEventStoreRepository eventStoreRepository, IEvent @event)
        {
            try
            {
                var oldMovieModel = JsonConvert.DeserializeObject<MovieModel>(@event.OldPayLoad);
                var newMovieModel = JsonConvert.DeserializeObject<MovieModel>(@event.NewPayLoad);

                if (oldMovieModel.ReleaseDate != newMovieModel.ReleaseDate)
                {
                    await eventStoreRepository?.AppendAsync(@event as EventModel);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}