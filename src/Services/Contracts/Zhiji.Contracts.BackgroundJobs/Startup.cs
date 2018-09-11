using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Zhiji.Common.Domain;
using Zhiji.Contracts.BackgroundJobs.BillingDateCheck;
using Zhiji.Contracts.Infrastructure;
using Zhiji.EventBus;
using Zhiji.EventBus.RabbitMQ;
using Zhiji.IntegrationEventLog;

namespace Zhiji.Contracts.BackgroundJobs
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureApplication(services);
            ConfigureEntityFramework(services);
            ConfigureEventBus(services);
            ConfigureLogging(services);
        }

        public void ConfigureApplication(IServiceCollection services)
        {
            services.Scan(s => s
                .FromAssemblyOf<ContractContext>()
                    .AddClasses(t => t.AssignableTo<IRepository>())
                        .AsImplementedInterfaces()
                    .AddClasses(t => t.AssignableTo<IQuery>())
                        .AsImplementedInterfaces());
                        
            services.Configure<BillingDateCheckOptions>(this.Configuration.GetSection("BillingDateCheck"));
            services.AddSingleton<IHostedService, BillingDateCheckJob>();
            services.AddSingleton<Func<DbConnection, IIntegrationEventService>>(c => new IntegrationEventService(c));
            services.AddSingleton(DateTimeZoneProviders.Tzdb);
            services.AddSingleton<IClock>(SystemClock.Instance);
        }

        public void ConfigureEntityFramework(IServiceCollection services)
        {
            services
                .AddEntityFrameworkSqlServer()
                .AddDbContextPool<ContractContext>(
                    opt => opt
                        .UseSqlServer(this.Configuration.GetConnectionString("Master"),
                            o => o.EnableRetryOnFailure()))
                .AddDbContextPool<ContractQueryContext>(
                    opt => opt
                        .UseSqlServer(this.Configuration.GetConnectionString("Query"),
                            o => o.EnableRetryOnFailure())
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
                .AddDbContextPool<IntegrationEventContext>(
                    opt => opt
                        .UseSqlServer(this.Configuration.GetConnectionString("Master"),
                            o => o.EnableRetryOnFailure()));
        }

        public void ConfigureEventBus(IServiceCollection services)
        {
            services.AddSingleton<IEventBus, RabbitMQEventBus>();
            services.RegisterEasyNetQ(
                this.Configuration.GetConnectionString("RabbitMQ"),
                r => r.Register<EasyNetQ.ISerializer>(
                    new EasyNetQ.JsonSerializer(
                        new JsonSerializerSettings()
                            .ConfigureForNodaTime(DateTimeZoneProviders.Tzdb))));
        }

        public void ConfigureLogging(IServiceCollection services)
        {
            services.AddLogging(b =>
            {
                b.AddConfiguration(this.Configuration.GetSection("Logging"));
                b.AddConsole();
                b.AddDebug();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(context => context.Response.WriteAsync("Service is running"));
        }
    }
}
