using Api.Cores.Base.EventStores.Repository;
using Api.Infrastructures.EventStoreRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configurations
{
    public static class EventStoreServiceExtension
    {
        public static void AddEventStore(this IServiceCollection services)
        {
            services.AddTransient<IEventStoreRepository, EventStoreRepository>();
        }
    }
}