using System;
using System.Collections.Generic;
using System.Linq;
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
using Swashbuckle.AspNetCore.Swagger;
using Zhiji.Common.Api;
using Zhiji.Common.Domain;
using Zhiji.Contracts.Infrastructure;

namespace Zhiji.Contracts.Api
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
            ConfigureAutoMapper(services);
            ConfigureSwagger(services);

            services
                .AddRouting(o => o.LowercaseUrls = true)
                .AddMvc()
                .AddFluentValidation(o => o.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        public void ConfigureApplication(IServiceCollection services)
        {
            services.Scan(s => s
                .FromAssemblyOf<ContractContext>()
                    .AddClasses(t => t.AssignableTo<IRepository>())
                        .AsImplementedInterfaces()
                    .AddClasses(t => t.AssignableTo<IQuery>())
                        .AsImplementedInterfaces());
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
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
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
