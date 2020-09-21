using Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Cores.Repository.Reads
{
    public interface IGetMovieByTitleRepository
    {
        Task<IReadOnlyCollection<MovieModel>> GetMovieByTitleAsync(MovieModel movieModel);
    }
}