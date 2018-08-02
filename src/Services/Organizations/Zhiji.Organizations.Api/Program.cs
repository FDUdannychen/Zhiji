using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Zhiji.Common.Api;
using Zhiji.Common.Domain;
using Zhiji.Organizations.Domain.Employees;
using Zhiji.Organizations.Infrastructure;

namespace Zhiji.Organizations.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webHost = BuildWebHost(args);
            await webHost.EnsureDbContextAsync<OrganizationContext>(SeedOrganizationContext);
            await webHost.RunAsync();
        }

        static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        static async Task SeedOrganizationContext(OrganizationContext context, IServiceProvider services)
        {
            using (context)
            {
                await context.Database.EnsureCreatedAsync();

                if (!context.Set<EmployeeStatus>().Any())
                {
                    var tenementTypes = Enumeration.GetAll<EmployeeStatus>();
                    await context.Set<EmployeeStatus>().AddRangeAsync(tenementTypes);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
