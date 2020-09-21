using Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Cores.Repository.Reads
{
    public interface IGetMovieByReleaseDateRepository
    {
        Task<IReadOnlyCollection<MovieModel>> GetMovieByReleaseDateAsync(MovieModel movieModel);
    }
}