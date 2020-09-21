using Api.Business.EventStores.Handler;
using Api.Models;
using System.Threading.Tasks;

namespace Api.Cores.Repository.Writes
{
    public interface IMovieUpdateRepository : IDataEventStoreHandler<MovieModel, MovieModel>
    {
        Task<bool?> UpdateAsync(MovieModel movieModel);
    }
}