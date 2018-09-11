using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Zhiji.Common.AspNetCore;
using Zhiji.Common.EntityFrameworkCore;
using Zhiji.Customers.Domain.Tenements;
using Zhiji.Customers.Infrastructure;

namespace Zhiji.Customers.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webHost = BuildWebHost(args);
            await webHost.MigrateDbContextAsync<CustomerContext>(SeedCustomerContext);
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
                context.EnsureEnumeration<TenementType>();
                await context.SaveChangesAsync();
            }
        }
    }
}
