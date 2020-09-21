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
    public class MovieDeleteUnitTest
    {
        private Mock<IMovieDeleteRepository> movieDeleteRepositoryMock = null;
        private Mock<IEventBus> eventbusMock = null;
        private Mock<IMapper> mapperMock = null;
        private BackgroundQueue backgroundQueueMock = null;

        private readonly IMovieDeleteCommandHandler movieDeleteCommandHandler = null;

        public MovieDeleteUnitTest()
        {
            movieDeleteRepositoryMock = new Mock<IMovieDeleteRepository>();
            eventbusMock = new Mock<IEventBus>();
            backgroundQueueMock = new BackgroundQueue((e) => { }, 1, 1000);
            mapperMock = new Mock<IMapper>();

            movieDeleteCommandHandler = new MovieDeleteCommandHandler(movieDeleteRepositoryMock.Object, backgroundQueueMock, mapperMock.Object, eventbusMock.Object);
        }

        private MovieDeleteCommand MovieDeleteCommandData()
        {
            var movieDeleteCommand = new MovieDeleteCommand()
            {
                MovieIdentity = Guid.Parse("23E453D2-E6E6-40AF-88A7-0BCEFA513686"),
                AggregateId = Guid.Parse("94A1AAB9-1FC2-4EFC-9CFC-12AD6BB1480B"),
                StateId = Guid.NewGuid()
            };

            return movieDeleteCommand;
        }

        //[TestMethod]
        public void JsonData()
        {
            var json = JsonConvert.SerializeObject(this.MovieDeleteCommandData());
        }

        [TestMethod]
        public async Task DeleteMovie_Success_TestMethod()
        {
            var movieDeleteCommand = this.MovieDeleteCommandData();

            // Repository Setup
            movieDeleteRepositoryMock
                   .Setup((r) => r.DeleteAsync(It.IsAny<MovieModel>()))
                   .ReturnsAsync(true);

            var response = await movieDeleteCommandHandler.HandleAsync(movieDeleteCommand);

            Assert.IsTrue(Convert.ToBoolean(response));
        }

        [TestMethod]
        public async Task UpdateMovie_Title_Exists_TestMethod()
        {
            var movieDeleteCommand = this.MovieDeleteCommandData();

            // Repository Setup
            movieDeleteRepositoryMock
                   .Setup((r) => r.DeleteAsync(It.IsAny<MovieModel>()))
                   .ReturnsAsync(false);

            var response = await movieDeleteCommandHandler.HandleAsync(movieDeleteCommand);

            Assert.IsTrue(response is ErrorModel);
        }

        [TestMethod]
        public async Task UpdateMovie_Exception_TestMethod()
        {
            var movieDeleteCommand = this.MovieDeleteCommandData();

            // Repository Setup
            movieDeleteRepositoryMock
                  .Setup((r) => r.DeleteAsync(It.IsAny<MovieModel>()))
                  .Throws<Exception>();

            try
            {
                var response = await movieDeleteCommandHandler.HandleAsync(movieDeleteCommand);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }
    }
}