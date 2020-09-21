using Api.Business.Query.Handlers;
using Api.Business.Query.Queries;
using Api.Cores.Queries;
using Api.Cores.Repository.Reads;
using Api.Models;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class GetMovieByTitleUnitTest
    {
        private readonly Mock<IGetMovieByTitleRepository> getMovieByTitleRepositoryMock = null;
        private readonly Mock<IMapper> mapperMock;

        private readonly IGetMovieByTitleQueryHandler getMovieByTitleQueryHandler = null;

        public GetMovieByTitleUnitTest()
        {
            getMovieByTitleRepositoryMock = new Mock<IGetMovieByTitleRepository>();
            mapperMock = new Mock<IMapper>();

            getMovieByTitleQueryHandler = new GetMovieByTitleQueryHandler(getMovieByTitleRepositoryMock.Object, mapperMock.Object);
        }

        private GetMovieByTitleQuery GetMovieByTitleQueryData()
        {
            return new GetMovieByTitleQuery()
            {
                Title = "Hack"
            };
        }

        private IReadOnlyCollection<MovieModel> TestResponseData()
        {
            return new List<MovieModel>() { new MovieModel() { Title = "Hacker " } };
        }

        //[TestMethod]
        public void JsonData()
        {
            var jsonData = JsonConvert.SerializeObject(this.GetMovieByTitleQueryData());
        }

        [TestMethod]
        public async Task GetMovieByTitle_Success()
        {
            getMovieByTitleRepositoryMock
                .Setup(e => e.GetMovieByTitleAsync(It.IsAny<MovieModel>()))
                .ReturnsAsync(TestResponseData());

            var data = await getMovieByTitleQueryHandler?.HandleAsync(this.GetMovieByTitleQueryData());

            int count = ((IReadOnlyCollection<MovieModel>)data).Count;

            Assert.AreEqual<int>(1, count);
        }

        [TestMethod]
        public async Task GetMovieByTitle_Error_OnNull()
        {
            getMovieByTitleRepositoryMock
                .Setup(e => e.GetMovieByTitleAsync(It.IsAny<MovieModel>()))
                .ReturnsAsync((IReadOnlyCollection<MovieModel>)null);

            var data = await getMovieByTitleQueryHandler?.HandleAsync(this.GetMovieByTitleQueryData());

            Assert.IsTrue(data is ErrorModel);
        }

        [TestMethod]
        public async Task GetMovieByTitle_Error_OnZeroCount()
        {
            getMovieByTitleRepositoryMock
                  .Setup(e => e.GetMovieByTitleAsync(It.IsAny<MovieModel>()))
                  .ReturnsAsync(new List<MovieModel>());

            var data = await getMovieByTitleQueryHandler?.HandleAsync(this.GetMovieByTitleQueryData());

            Assert.IsTrue(data is ErrorModel);
        }
    }
}