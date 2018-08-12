﻿using System;
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
using Zhiji.Contracts.BackgroundJobs.BillGeneration;
using Zhiji.Contracts.Infrastructure;

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

            services.Configure<BillGenerationOptions>(this.Configuration.GetSection("BillGeneration"));
            services.AddSingleton<IHostedService, BillGenerationJob>();
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
