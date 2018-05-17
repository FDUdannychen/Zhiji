using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zhiji.Common.AspNetCore;
using Zhiji.Organization.Infrastructure;

namespace Zhiji.Organization.Api.Test
{
    public abstract class OrganizationTestBase
    {
        public static class Get
        {
            public static string BranchById(int id) => $"branch/{id}";
        }

        protected async Task<TestServer> CreateServerAsync()
        {
            var builder = WebHost
                .CreateDefaultBuilder()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(b => b.AddJsonFile("appsettings.json"));

            var server = new TestServer(builder);
            await server.Host.EnsureDbContextAsync<OrganizationContext>();
            return server;
        }
    }
}
