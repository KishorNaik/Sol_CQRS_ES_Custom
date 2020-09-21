using Api.Applications.Api.Queries;
using Api.Applications.ApiQueries;
using Api.Cores.Api.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configurations
{
    public static class ApiQueryHandlerServiceExtension
    {
        public static void AddApiQuery(this IServiceCollection services)
        {
            services.AddTransient<IGetAllMovieApiQueryHandler, GetAllMovieApiCommandHandler>();
            services.AddTransient<IGetMovieByTitleApiQueryHandler, GetMovieByTitleApiQueryHandler>();
            services.AddTransient<IGetMovieByReleaseDateApiQueryHandler, GetMovieByReleaseDateApiQueryHandler>();
        }
    }
}