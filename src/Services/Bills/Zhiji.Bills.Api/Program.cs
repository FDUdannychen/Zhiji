using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Zhiji.Bills.Domain.Bills;
using Zhiji.Bills.Infrastructure;
using Zhiji.Common.AspNetCore;
using Zhiji.Common.EntityFrameworkCore;
using Zhiji.IntegrationEventLog;

namespace Zhiji.Bills.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webHost = BuildWebHost(args);
            await webHost.MigrateDbContextAsync<BillContext>(SeedBillContext);
            await webHost.MigrateDbContextAsync<IntegrationEventContext>();
            await webHost.RunAsync();
        }

        static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        static async Task SeedBillContext(BillContext context, IServiceProvider services)
        {
            context.EnsureEnumeration<BillStatus>();
            await context.SaveChangesAsync();
        }
    }
}
