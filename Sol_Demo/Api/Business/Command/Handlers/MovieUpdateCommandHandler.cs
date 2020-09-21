using Api.Business.Command.Abstracts;
using Api.Business.Command.Commands;
using Api.Business.Event.Handlers;
using Api.Cores.Base.Commands.Handler;
using Api.Cores.Base.Events;
using Api.Cores.Base.EventStores.Model;
using Api.Cores.Commands;
using Api.Cores.Repository.Writes;
using Api.Models;
using AutoMapper;
using DalSoft.Hosting.BackgroundQueue;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Api.Business.Command.Handlers
{
    public sealed class MovieUpdateCommandHandler : MovieBaseCommandAbstract, IMovieUpdateCommandHandler
    {
        private readonly IMovieUpdateRepository movieUpdateRepository = null;
        private readonly IMapper mapper = null;
        private readonly BackgroundQueue backgroundQueue = null;
        private readonly IEventBus eventBus = null;

        public MovieUpdateCommandHandler(
            IMovieUpdateRepository movieUpdateRepository,
            IMapper mapper,
            BackgroundQueue backgroundQueue,
            IEventBus eventBus
            )
        {
            this.movieUpdateRepository = movieUpdateRepository;
            this.mapper = mapper;
            this.backgroundQueue = backgroundQueue;
            this.eventBus = eventBus;
        }

        async Task<object> ICommandHandler<MovieUpdateCommand, object>.HandleAsync(MovieUpdateCommand command)
        {
            try
            {
                // Subscribe Data Event Store.
                movieUpdateRepository.DataEventStoreHandler += MovieUpdateRepository_DataEventStoreHandler;

                // Call update Repository
                var repositoryResponse = await movieUpdateRepository?.UpdateAsync(this.mapper.Map<MovieModel>(command));

                if (repositoryResponse == false) return await base.MovieExistMessageAsync();

                return repositoryResponse;
            }
            catch
            {
                throw;
            }
            finally
            {
                movieUpdateRepository.DataEventStoreHandler -= MovieUpdateRepository_DataEventStoreHandler;
            }
        }

        private void MovieUpdateRepository_DataEventStoreHandler(object sender, MovieModel oldMovieModel, MovieModel newMovieModel)
        {
            backgroundQueue?.Enqueue(async (cancellationToken) =>
           {
               await
               eventBus
               .RegisterEvent(new EventModel()
               {
                   AggregateId = newMovieModel.AggregateId,
                   StateId = newMovieModel.StateId,
                   EventName = "MovieTitleChanged",
                   NewPayLoad = JsonConvert.SerializeObject(newMovieModel),
                   OldPayLoad = JsonConvert.SerializeObject(oldMovieModel),
                   CreatedDate = DateTime.Now
               }, new MovieTitleChangedEventHandler())
               .RegisterEvent(new EventModel()
               {
                   AggregateId = newMovieModel.AggregateId,
                   StateId = newMovieModel.StateId,
                   EventName = "MovieReleaseDateChanged",
                   NewPayLoad = JsonConvert.SerializeObject(newMovieModel),
                   OldPayLoad = JsonConvert.SerializeObject(oldMovieModel),
                   CreatedDate = DateTime.Now
               }, new MovieReleaseDateChangedEventHandler())
               .RegisterEvent(new EventModel()
               {
                   AggregateId = newMovieModel.AggregateId,
                   StateId = newMovieModel.StateId,
                   EventName = "MovieUpdate",
                   NewPayLoad = JsonConvert.SerializeObject(newMovieModel),
                   OldPayLoad = JsonConvert.SerializeObject(oldMovieModel),
                   CreatedDate = DateTime.Now
               }, new MovieUpdatedEventHandler())
               .BroadcastEventsAsync();
           });
        }
    }
}