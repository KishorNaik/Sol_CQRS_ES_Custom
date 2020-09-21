using Api.Cores.Repository.Reads;
using Api.Cores.Repository.Writes;
using Api.Infrastructures.Repository.Reads;
using Api.Infrastructures.Repository.Writes;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configurations
{
    public static class RepositoryServiceExtension
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IMovieCreateRepository, MovieCreateRepository>();
            services.AddTransient<IMovieUpdateRepository, MovieUpdateRepository>();
            services.AddTransient<IMovieDeleteRepository, MovieDeleteRepository>();

            services.AddTransient<IGetMovieByReleaseDateRepository, GetMovieByReleaseDateRepository>();
            services.AddTransient<IGetAllMovieRepository, GetAllMovieRepository>();
            services.AddTransient<IGetMovieByTitleRepository, GetMovieByTitleRepository>();
        }
    }
}