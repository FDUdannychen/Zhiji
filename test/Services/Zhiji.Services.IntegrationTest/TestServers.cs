using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Nito.AsyncEx;
using Zhiji.Common.AspNetCore;
using Zhiji.Organizations.Api;
using Zhiji.Organizations.Infrastructure;

namespace Zhiji.Services.IntegrationTest
{
    class TestServers
    {
        public static readonly AsyncLazy<TestServer> OrganizationApiServer = new AsyncLazy<TestServer>(async () =>
        {
            var builder = WebHost
                .CreateDefaultBuilder()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(b => b.AddJsonFile("appsettings.json"));

            var server = new TestServer(builder);
            await server.Host.MigrateDbContextAsync<OrganizationContext>(async (context, services) =>
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
            });
            return server;
        });
    }
}
