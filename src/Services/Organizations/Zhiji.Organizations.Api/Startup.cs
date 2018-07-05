using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Zhiji.Common.Domain;
using Zhiji.Organizations.Infrastructure;

namespace Zhiji.Organizations.Api
{
    public partial class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureApplication(services);
            ConfigureMediator(services);
            ConfigureEntityFramework(services);
            ConfigureAutoMapper(services);

            services.AddRouting(o => o.LowercaseUrls = true);
            services.AddMvc();
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new Info { Title = "Organization API", Version = "v1" });
            });
        }

        public void ConfigureApplication(IServiceCollection services)
        {
            services.Scan(s => s
                .FromAssemblyOf<OrganizationContext>()
                .AddClasses(t => t.AssignableTo<IRepository>())
                .AsImplementedInterfaces());
        }

        public void ConfigureMediator(IServiceCollection services)
        {
            services.AddScoped<ServiceFactory>(p => p.GetService);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));

            services.Scan(s => s
                .FromAssembliesOf(typeof(IMediator), typeof(Startup))
                .AddClasses()
                .AsImplementedInterfaces());
        }

        public void ConfigureEntityFramework(IServiceCollection services)
        {
            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<OrganizationContext>(
                    opt => opt.UseSqlServer(this.Configuration["ConnectionString"],
                        o => o.EnableRetryOnFailure()));
        }

        public void ConfigureAutoMapper(IServiceCollection services)
        {
            Mapper.Reset();
            services.AddAutoMapper();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(o =>
            {
                o.PreSerializeFilters.Add((document, request) =>
                {
                    document.Paths = document.Paths
                        .ToDictionary(p => p.Key.ToLowerInvariant(), p => p.Value);
                });
            });

            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "Organization API");
                o.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
