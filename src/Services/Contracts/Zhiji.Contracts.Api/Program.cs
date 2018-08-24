using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.AspNetCore;
using Zhiji.Common.EntityFrameworkCore;
using Zhiji.Contracts.Domain.Templates;
using Zhiji.Contracts.Infrastructure;
using Zhiji.IntegrationEventLog;

namespace Zhiji.Contracts.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webHost = BuildWebHost(args);
            await webHost.EnsureDbContextAsync<ContractContext>(SeedContractContext);
            await webHost.EnsureDbContextAsync<IntegrationEventContext>(async (c, p) =>
            {
                using (c)
                {
                    await c.Database.MigrateAsync();
                }
            });
            await webHost.RunAsync();
        }

        static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        static async Task SeedContractContext(ContractContext context, IServiceProvider services)
        {
            using (context)
            {
                await context.Database.EnsureCreatedAsync();
                context.EnsureEnumeration<BillingMode>();
                await context.SaveChangesAsync();
            }
        }
    }
}
