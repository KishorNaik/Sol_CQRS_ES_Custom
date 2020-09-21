using Api.Business.Command.Handlers;
using Api.Cores.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configurations
{
    public static class CommmandHandlerServiceExtension
    {
        public static void AddCommandHandler(this IServiceCollection services)
        {
            services.AddTransient<IMovieCreateCommandHandler, MovieCreateCommandHandler>();
            services.AddTransient<IMovieUpdateCommandHandler, MovieUpdateCommandHandler>();
            services.AddTransient<IMovieDeleteCommandHandler, MovieDeleteCommandHandler>();
        }
    }
}