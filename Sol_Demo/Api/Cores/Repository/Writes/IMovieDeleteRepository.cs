using Api.Business.EventStores.Handler;
using Api.Models;
using System.Threading.Tasks;

namespace Api.Cores.Repository.Writes
{
    public interface IMovieDeleteRepository : IDataEventStoreHandler<MovieModel>
    {
        Task<bool?> DeleteAsync(MovieModel movieModel);
    }
}