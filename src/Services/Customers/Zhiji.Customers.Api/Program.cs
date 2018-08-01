using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Zhiji.Common.Api;
using Zhiji.Common.Domain;
using Zhiji.Customers.Domain.Tenements;
using Zhiji.Customers.Infrastructure;

namespace Zhiji.Customers.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webHost = BuildWebHost(args);
            await webHost.EnsureDbContextAsync<CustomerContext>(SeedCustomerContext);
            await webHost.RunAsync();
        }

        static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        static async Task SeedCustomerContext(CustomerContext context, IServiceProvider services)
        {
            using (context)
            {
                await context.Database.EnsureCreatedAsync();

                if (!context.TenementTypes.Any())
                {
                    var tenementTypes = Enumeration.GetAll<TenementType>();
                    await context.TenementTypes.AddRangeAsync(tenementTypes);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
