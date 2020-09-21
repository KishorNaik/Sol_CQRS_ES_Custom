using Api.Business.Query.Handlers;
using Api.Cores.Queries;
using Api.Cores.Repository.Reads;
using Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class GetAllMovieUnitTest
    {
        private readonly Mock<IGetAllMovieRepository> getAllMovieRepositoryMock = null;

        private readonly IGetAllMovieQueryHandler getAllMovieQueryHandler = null;

        public GetAllMovieUnitTest()
        {
            getAllMovieRepositoryMock = new Mock<IGetAllMovieRepository>();

            getAllMovieQueryHandler = new GetAllMovieQueryHandler(getAllMovieRepositoryMock.Object);
        }

        private IReadOnlyCollection<MovieModel> TestResponseData()
        {
            return new List<MovieModel>() { new MovieModel() { Title = "Test " } };
        }

        [TestMethod]
        public async Task GetAllMovie_Success()
        {
            getAllMovieRepositoryMock
                .Setup(e => e.GetAllMovieAsync())
                .ReturnsAsync(TestResponseData());

            var data = await getAllMovieQueryHandler?.HandleAsync();

            int count = ((IReadOnlyCollection<MovieModel>)data).Count;

            Assert.AreEqual<int>(1, count);
        }

        [TestMethod]
        public async Task GetAllMovie_Error_OnNull()
        {
            getAllMovieRepositoryMock
                .Setup(e => e.GetAllMovieAsync())
                .ReturnsAsync((IReadOnlyCollection<MovieModel>)null);

            var data = await getAllMovieQueryHandler?.HandleAsync();

            Assert.IsTrue(data is ErrorModel);
        }

        [TestMethod]
        public async Task GetAllMovie_Error_OnZeroCount()
        {
            getAllMovieRepositoryMock
                .Setup(e => e.GetAllMovieAsync())
                .ReturnsAsync(new List<MovieModel>());

            var data = await getAllMovieQueryHandler?.HandleAsync();

            Assert.IsTrue(data is ErrorModel);
        }
    }
}