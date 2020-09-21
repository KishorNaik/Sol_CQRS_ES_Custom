using Api.Applications.ApiCommands;
using Api.Cores.Api.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configurations
{
    public static class ApiCommandHandlerServiceExtension
    {
        public static void AddApiCommand(this IServiceCollection services)
        {
            services.AddTransient<IMovieCreateApiCommandHandler, MovieCreateApiCommandHandler>();
            services.AddTransient<IMovieUpdateApiCommandHandler, MovieUpdateApiCommandHandler>();
            services.AddTransient<IMovieDeleteApiCommandHandler, MovieDeleteApiCommandHandler>();
        }
    }
}