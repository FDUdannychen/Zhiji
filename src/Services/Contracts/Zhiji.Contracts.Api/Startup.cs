using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MicroElements.Swashbuckle.NodaTime;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Swashbuckle.AspNetCore.Swagger;
using Zhiji.Common.AspNetCore;
using Zhiji.Common.Domain;
using Zhiji.Contracts.Infrastructure;
using Zhiji.IntegrationEventLog;

namespace Zhiji.Contracts.Api
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
            ConfigureAutoMapper(services);
            ConfigureSwagger(services);

            services
                .AddRouting(o => o.LowercaseUrls = true)
                .AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Latest)
                    .AddJsonOptions(o => o.SerializerSettings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb))
                .AddFluentValidation(o => o.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        public void ConfigureApplication(IServiceCollection services)
        {
            services.Scan(s => s
                .FromAssemblyOf<ContractContext>()
                    .AddClasses(t => t.AssignableTo<IRepository>())
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
                    .AddClasses(t => t.AssignableTo<IQuery>())
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

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
                    o.SwaggerDoc("v1", new Info { Title = "Contract API", Version = "v1" });
                    o.AddFluentValidationRules();
                    o.DocumentFilter<LowerCaseDocumentFilter>();
                    o.ConfigureForNodaTime();
                });
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
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "Contract API");
                o.RoutePrefix = string.Empty;
            });

            app.UseMvc();            
        }
    }
}
