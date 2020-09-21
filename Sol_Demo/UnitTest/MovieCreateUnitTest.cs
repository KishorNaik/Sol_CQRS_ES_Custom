using Api.Business.Command.Commands;
using Api.Business.Command.Handlers;
using Api.Cores.Base.Events;
using Api.Cores.Commands;
using Api.Cores.Repository.Writes;
using Api.Models;
using AutoMapper;
using DalSoft.Hosting.BackgroundQueue;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class MovieCreateUnitTest
    {
        private Mock<IMovieCreateRepository> movieCreateRepositoryMock = null;
        private Mock<IEventBus> eventbusMock = null;
        private Mock<IMapper> mapperMock = null;
        private BackgroundQueue backgroundQueueMock = null;

        private IMovieCreateCommandHandler movieCreateCommandHandler = null;

        public MovieCreateUnitTest()
        {
            movieCreateRepositoryMock = new Mock<IMovieCreateRepository>();
            eventbusMock = new Mock<IEventBus>();
            backgroundQueueMock = new BackgroundQueue((e) => { }, 1, 1000);
            mapperMock = new Mock<IMapper>();

            movieCreateCommandHandler =
                new MovieCreateCommandHandler
                        (
                            movieCreateRepositoryMock.Object,
                            eventbusMock.Object,
                            mapperMock.Object,
                            backgroundQueueMock
                        );
        }

        // Generate Json for Testing Api on Postman
        public MovieCreateCommand MovieCreateCommandData()
        {
            var movieCreateCommand = new MovieCreateCommand()
            {
                Title = "Hacker",
                ReleaseDate = new System.DateTime(1995, 1, 1),
                AggregateId = Guid.NewGuid(),
                StateId = Guid.NewGuid()
            };

            return movieCreateCommand;
        }

        //[TestMethod]
        public void JsonData()
        {
            var json = JsonConvert.SerializeObject(this.MovieCreateCommandData());
        }

        [TestMethod]
        public async Task AddMovie_Success_TestMethod()
        {
            var movieCreateCommand = this.MovieCreateCommandData();

            // Repository Setup
            movieCreateRepositoryMock
                   .Setup((r) => r.CreateAsync(It.IsAny<MovieModel>()))
                   .ReturnsAsync(true);

            var response = await movieCreateCommandHandler.HandleAsync(movieCreateCommand);

            Assert.IsTrue(Convert.ToBoolean(response));
        }

        [TestMethod]
        public async Task AddMovie_Title_Exists_TestMethod()
        {
            var movieCreateCommand = this.MovieCreateCommandData();

            // Repository Setup
            movieCreateRepositoryMock
                   .Setup((r) => r.CreateAsync(It.IsAny<MovieModel>()))
                   .ReturnsAsync(false);

            var response = await movieCreateCommandHandler.HandleAsync(movieCreateCommand);

            Assert.IsTrue(response is ErrorModel);
        }

        [TestMethod]
        public async Task AddMovie_Repository_Exception_Throw_TestMethod()
        {
            var movieCreateCommand = this.MovieCreateCommandData();

            // Repository Setup
            movieCreateRepositoryMock
                   .Setup((r) => r.CreateAsync(It.IsAny<MovieModel>()))
                   .Throws<Exception>();

            try
            {
                var response = await movieCreateCommandHandler.HandleAsync(movieCreateCommand);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }
    }
}