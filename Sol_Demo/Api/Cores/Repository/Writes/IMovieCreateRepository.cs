using Api.Business.EventStores.Handler;
using Api.Models;
using System.Threading.Tasks;

namespace Api.Cores.Repository.Writes
{
    public interface IMovieCreateRepository : IDataEventStoreHandler<MovieModel>
    {
        Task<bool?> CreateAsync(MovieModel movieModel);
    }
}