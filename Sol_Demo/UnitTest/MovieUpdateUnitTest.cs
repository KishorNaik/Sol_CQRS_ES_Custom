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
    public class MovieUpdateUnitTest
    {
        private Mock<IMovieUpdateRepository> movieUpdateRepositoryMock = null;
        private Mock<IEventBus> eventbusMock = null;
        private Mock<IMapper> mapperMock = null;
        private BackgroundQueue backgroundQueueMock = null;

        private readonly IMovieUpdateCommandHandler movieUpdateCommandHandler = null;

        public MovieUpdateUnitTest()
        {
            movieUpdateRepositoryMock = new Mock<IMovieUpdateRepository>();
            eventbusMock = new Mock<IEventBus>();
            backgroundQueueMock = new BackgroundQueue((e) => { }, 1, 1000);
            mapperMock = new Mock<IMapper>();

            movieUpdateCommandHandler = new MovieUpdateCommandHandler(movieUpdateRepositoryMock.Object, mapperMock.Object, backgroundQueueMock, eventbusMock.Object);
        }

        private MovieUpdateCommand MovieUpdateCommandData()
        {
            var movieUpdateCommand = new MovieUpdateCommand()
            {
                MovieIdentity = Guid.Parse("CAD67740-C13D-4E74-9DF2-9233B4C59390"),
                Title = "Who am I",
                ReleaseDate = new System.DateTime(1995, 1, 1),
                AggregateId = Guid.Parse("94A1AAB9-1FC2-4EFC-9CFC-12AD6BB1480B"),
                StateId = Guid.NewGuid()
            };

            return movieUpdateCommand;
        }

        //[TestMethod]
        public void JsonData()
        {
            var json = JsonConvert.SerializeObject(this.MovieUpdateCommandData());
        }

        [TestMethod]
        public async Task UpdateMovie_Success_TestMethod()
        {
            var movieUpdateCommand = this.MovieUpdateCommandData();

            // Repository Setup
            movieUpdateRepositoryMock
                   .Setup((r) => r.UpdateAsync(It.IsAny<MovieModel>()))
                   .ReturnsAsync(true);

            var response = await movieUpdateCommandHandler.HandleAsync(movieUpdateCommand);

            Assert.IsTrue(Convert.ToBoolean(response));
        }

        [TestMethod]
        public async Task UpdateMovie_Title_Exists_TestMethod()
        {
            var movieUpdateCommand = this.MovieUpdateCommandData();

            // Repository Setup
            movieUpdateRepositoryMock
                   .Setup((r) => r.UpdateAsync(It.IsAny<MovieModel>()))
                   .ReturnsAsync(false);

            var response = await movieUpdateCommandHandler.HandleAsync(movieUpdateCommand);

            Assert.IsTrue(response is ErrorModel);
        }

        [TestMethod]
        public async Task UpdateMovie_Exception_TestMethod()
        {
            var movieUpdateCommand = this.MovieUpdateCommandData();

            // Repository Setup
            movieUpdateRepositoryMock
                   .Setup((r) => r.UpdateAsync(It.IsAny<MovieModel>()))
                   .Throws<Exception>();

            try
            {
                var response = await movieUpdateCommandHandler.HandleAsync(movieUpdateCommand);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }
    }
}