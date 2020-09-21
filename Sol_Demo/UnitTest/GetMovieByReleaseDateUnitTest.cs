using Api.Business.Query.Handlers;
using Api.Business.Query.Queries;
using Api.Cores.Queries;
using Api.Cores.Repository.Reads;
using Api.Models;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class GetMovieByReleaseDateUnitTest
    {
        private readonly Mock<IGetMovieByReleaseDateRepository> getMovieByReleaseDateRepositoryMock;
        private readonly Mock<IMapper> mapperMock;

        private readonly IGetMovieByReleaseDateQueryHandler getMovieByReleaseDateQueryHandler = null;

        public GetMovieByReleaseDateUnitTest()
        {
            getMovieByReleaseDateRepositoryMock = new Mock<IGetMovieByReleaseDateRepository>();
            mapperMock = new Mock<IMapper>();

            getMovieByReleaseDateQueryHandler = new GetMovieByReleaseDateQueryHandler(getMovieByReleaseDateRepositoryMock.Object, mapperMock.Object);
        }

        private GetMovieByReleaseDateQuery GetMovieByReleaseDateQueryData()
        {
            return new GetMovieByReleaseDateQuery()
            {
                ReleaseStartDate = new DateTime(1990, 1, 1),
                ReleaseEndDate = new DateTime(2020, 09, 20)
            };
        }

        private IReadOnlyCollection<MovieModel> TestResponseData()
        {
            return new List<MovieModel>() { new MovieModel() { Title = "Hacker" } };
        }

        [TestMethod]
        public void JsonData()
        {
            var jsonData = JsonConvert.SerializeObject(this.GetMovieByReleaseDateQueryData());
        }

        [TestMethod]
        public async Task GetMovieByReleaseDate_Success()
        {
            getMovieByReleaseDateRepositoryMock
                .Setup(e => e.GetMovieByReleaseDateAsync(It.IsAny<MovieModel>()))
                .ReturnsAsync(TestResponseData());

            var data = await getMovieByReleaseDateQueryHandler?.HandleAsync(this.GetMovieByReleaseDateQueryData());

            int count = ((IReadOnlyCollection<MovieModel>)data).Count;

            Assert.AreEqual<int>(1, count);
        }

        [TestMethod]
        public async Task GetMovieByReleaseDate_Error_OnNull()
        {
            getMovieByReleaseDateRepositoryMock
               .Setup(e => e.GetMovieByReleaseDateAsync(It.IsAny<MovieModel>()))
               .ReturnsAsync((IReadOnlyCollection<MovieModel>)null);

            var data = await getMovieByReleaseDateQueryHandler?.HandleAsync(this.GetMovieByReleaseDateQueryData());

            Assert.IsTrue(data is ErrorModel);
        }

        [TestMethod]
        public async Task GetMovieByTitle_Error_OnZeroCount()
        {
            getMovieByReleaseDateRepositoryMock
                .Setup(e => e.GetMovieByReleaseDateAsync(It.IsAny<MovieModel>()))
                .ReturnsAsync(new List<MovieModel>());

            var data = await getMovieByReleaseDateQueryHandler?.HandleAsync(this.GetMovieByReleaseDateQueryData());

            Assert.IsTrue(data is ErrorModel);
        }
    }
}