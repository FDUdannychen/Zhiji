using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Zhiji.Common.Domain;
using Zhiji.Bills.BackgroundJobs.BillGeneration;
using Zhiji.Bills.Infrastructure;

namespace Zhiji.Bills.BackgroundJobs
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
            ConfigureLogging(services);
        }

        public void ConfigureApplication(IServiceCollection services)
        {
            services.Scan(s => s
                .FromAssemblyOf<BillContext>()
                .AddClasses(t => t.AssignableTo<IRepository>())
                .AsImplementedInterfaces());

            services.Configure<BillGenerationOptions>(this.Configuration.GetSection("BillGeneration"));
            services.AddSingleton<IHostedService, BillGenerationJob>();
        }

        public void ConfigureEntityFramework(IServiceCollection services)
        {
            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<BillContext>(
                    opt => opt.UseSqlServer(this.Configuration["ConnectionString"],
                        o => o.EnableRetryOnFailure()));
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
