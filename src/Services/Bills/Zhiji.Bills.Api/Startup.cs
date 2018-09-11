using System.Linq;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using MicroElements.Swashbuckle.NodaTime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Swashbuckle.AspNetCore.Swagger;
using Zhiji.Bills.Infrastructure;
using Zhiji.Common.AspNetCore;
using Zhiji.Common.Domain;
using Zhiji.EventBus;
using Zhiji.EventBus.IntegrationEvents;
using Zhiji.EventBus.RabbitMQ;
using Zhiji.IntegrationEventLog;

namespace Zhiji.Bills.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureApplication(services);
            ConfigureEntityFramework(services);
            ConfigureEventBus(services);
            ConfigureAutoMapper(services);
            ConfigureSwagger(services);
            ConfigureNodaTime(services);
            ConfigureMediator(services);
            ConfigureWeb(services);
        }

        public void ConfigureApplication(IServiceCollection services)
        {
            services.Scan(s => s
                .FromAssemblyOf<BillContext>()
                    .AddClasses(t => t.AssignableTo<IRepository>())
                        .AsImplementedInterfaces()
                    .AddClasses(t => t.AssignableTo<IQuery>())
                        .AsImplementedInterfaces());     
        }

        public void ConfigureEntityFramework(IServiceCollection services)
        {
            services
                .AddEntityFrameworkSqlServer()
                .AddDbContextPool<BillContext>(
                    opt => opt
                        .UseSqlServer(this.Configuration.GetConnectionString("Master"),
                            o => o.EnableRetryOnFailure()))
                .AddDbContextPool<BillQueryContext>(
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

        public void ConfigureAutoMapper(IServiceCollection services)
        {
            Mapper.Reset();
            services.AddAutoMapper();
        }

        public void ConfigureSwagger(IServiceCollection services)
        {
            services
                .AddSwaggerGen(o =>
                {
                    o.SwaggerDoc("v1", new Info { Title = "Bill API", Version = "v1" });
                    o.AddFluentValidationRules();
                    o.DocumentFilter<LowerCaseDocumentFilter>();
                    o.ConfigureForNodaTime();
                });
        }

        public void ConfigureNodaTime(IServiceCollection services)
        {
            services.AddSingleton(DateTimeZoneProviders.Tzdb);
            services.AddSingleton<IClock>(SystemClock.Instance);
        }

        public void ConfigureMediator(IServiceCollection services)
        {
            services.AddMediatR();
        }

        public void ConfigureWeb(IServiceCollection services)
        {
            services
                .AddRouting(o => o.LowercaseUrls = true)
                .AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Latest)
                    .AddJsonOptions(o => o.SerializerSettings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb))
                .AddFluentValidation(o => o.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "Bill API");
                o.RoutePrefix = string.Empty;
            });

            app.UseMvc();

            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<BillingDateReachedIntegrationEvent>("Zhiji.Bills.Api");
        }
    }
}
