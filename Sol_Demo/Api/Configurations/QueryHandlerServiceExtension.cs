using Api.Business.Query.Handlers;
using Api.Cores.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configurations
{
    public static class QueryHandlerServiceExtension
    {
        public static void AddQuery(this IServiceCollection services)
        {
            services.AddTransient<IGetAllMovieQueryHandler, GetAllMovieQueryHandler>();
            services.AddTransient<IGetMovieByReleaseDateQueryHandler, GetMovieByReleaseDateQueryHandler>();
            services.AddTransient<IGetMovieByTitleQueryHandler, GetMovieByTitleQueryHandler>();
        }
    }
}