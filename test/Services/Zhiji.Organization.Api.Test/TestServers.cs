using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zhiji.Common.AspNetCore;
using Zhiji.Organization.Infrastructure;

namespace Zhiji.Organization.Api.Test
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
            await server.Host.EnsureDbContextAsync<OrganizationContext>(async (context, services) =>
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
            });
            return server;
        });
    }
}
