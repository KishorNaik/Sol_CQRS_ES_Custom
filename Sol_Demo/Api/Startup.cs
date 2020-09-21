using Api.Configurations;
using Api.Cores.SqlDbProviders;
using AutoMapper;
using DalSoft.Hosting.BackgroundQueue.DependencyInjection;
using DapperFluent.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAutoMapper(typeof(Startup));

            services.AddBackgroundQueue(maxConcurrentCount: 3, millisecondsToWaitBeforePickingUpTask: 1000, onException: (ex) =>
            {
                Debug.WriteLine(ex.Message);
            });

            services.AddDapperFluent();

            services.AddTransient<ISqlClientDbProvider, SqlClientDbProvider>();

            services.AddEventBus();

            services.AddEventStore();

            services.AddApiCommand();

            services.AddApiQuery();

            services.AddCommandHandler();

            services.AddQuery();

            services.AddRepository();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}