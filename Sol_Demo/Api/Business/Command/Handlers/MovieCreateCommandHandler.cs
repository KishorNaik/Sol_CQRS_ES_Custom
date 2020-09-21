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
    public sealed class MovieCreateCommandHandler : MovieBaseCommandAbstract, IMovieCreateCommandHandler
    {
        private readonly IMovieCreateRepository movieCreateRepository = null;
        private readonly IEventBus eventBus = null;
        private readonly IMapper mapper = null;
        private readonly BackgroundQueue backgroundQueue = null;

        public MovieCreateCommandHandler(
            IMovieCreateRepository movieCreateRepository,
            IEventBus eventBus,
            IMapper mapper,
            BackgroundQueue backgroundQueue
            )
        {
            this.movieCreateRepository = movieCreateRepository;
            this.eventBus = eventBus;
            this.mapper = mapper;
            this.backgroundQueue = backgroundQueue;
        }

        async Task<object> ICommandHandler<MovieCreateCommand, object>.HandleAsync(MovieCreateCommand command)
        {
            try
            {
                movieCreateRepository.DataEventStoreHandler += MovieCreateRepository_DataEventStoreHandler;
                var repositoryResponse = await movieCreateRepository?.CreateAsync(mapper.Map<MovieModel>(command));

                if (repositoryResponse == false) return await base.MovieExistMessageAsync();

                return repositoryResponse;
            }
            catch
            {
                throw;
            }
            finally
            {
                movieCreateRepository.DataEventStoreHandler -= MovieCreateRepository_DataEventStoreHandler;
            }
        }

        private void MovieCreateRepository_DataEventStoreHandler(object sender, MovieModel newData)
        {
            // Run event using background task.
            backgroundQueue.Enqueue((cancellationToken) =>
            {
                eventBus
                .RegisterEvent(new EventModel()
                {
                    AggregateId = newData.AggregateId,
                    StateId = newData.StateId,
                    EventName = "MovieCreated",
                    NewPayLoad = JsonConvert.SerializeObject(newData),
                    CreatedDate = DateTime.Now
                }, new MovieCreatedEventHandler())
                .BroadcastEventsAsync();

                return Task.CompletedTask;
            });
        }
    }
}