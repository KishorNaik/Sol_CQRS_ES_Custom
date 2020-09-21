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
    public sealed class MovieDeleteCommandHandler : MovieBaseCommandAbstract, IMovieDeleteCommandHandler
    {
        private readonly IMovieDeleteRepository movieDeleteRepository = null;
        private readonly BackgroundQueue backgroundQueue = null;
        private readonly IMapper mapper = null;
        private readonly IEventBus eventBus = null;

        public MovieDeleteCommandHandler
            (
                IMovieDeleteRepository movieDeleteRepository,
                BackgroundQueue backgroundQueue,
                IMapper mapper = null,
                IEventBus eventBus = null
            )
        {
            this.movieDeleteRepository = movieDeleteRepository;
            this.backgroundQueue = backgroundQueue;
            this.mapper = mapper;
            this.eventBus = eventBus;
        }

        protected override Task<ErrorModel> MovieExistMessageAsync()
        {
            try
            {
                return Task.Run<ErrorModel>(() =>
                {
                    return new ErrorModel()
                    {
                        Message = "Something went wrong",
                        StatusCode = 401
                    };
                });
            }
            catch
            {
                throw;
            }
        }

        async Task<object> ICommandHandler<MovieDeleteCommand, object>.HandleAsync(MovieDeleteCommand command)
        {
            try
            {
                this.movieDeleteRepository.DataEventStoreHandler += MovieDeleteRepository_DataEventStoreHandler;
                var repositoryResponse = await movieDeleteRepository?.DeleteAsync(this.mapper.Map<MovieModel>(command));

                if (repositoryResponse == false) return await this.MovieExistMessageAsync();

                return repositoryResponse;
            }
            catch
            {
                throw;
            }
        }

        private void MovieDeleteRepository_DataEventStoreHandler(object sender, Models.MovieModel deleteModel)
        {
            backgroundQueue?.Enqueue(async (cancellationToken) =>
            {
                try
                {
                    await
                    eventBus
                    ?.RegisterEvent(new EventModel()
                    {
                        AggregateId = deleteModel.AggregateId,
                        StateId = deleteModel.StateId,
                        EventName = "MovieDeleted",
                        NewPayLoad = JsonConvert.SerializeObject(deleteModel),
                        CreatedDate = DateTime.Now
                    }, new MovieDeletedEventHandler())
                    ?.BroadcastEventsAsync();
                }
                catch
                {
                    throw;
                }
            });
        }
    }
}