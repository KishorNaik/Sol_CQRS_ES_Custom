using Api.Cores.Base.Events;
using Api.Cores.Base.Events.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configurations
{
    public static class EventBusServiceExtension
    {
        public static void AddEventBus(this IServiceCollection services)
        {
            services.AddTransient<IEventBus, EventBus>();
        }
    }
}