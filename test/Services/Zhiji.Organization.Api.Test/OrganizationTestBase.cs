﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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
            public static string BranchById(int id) => $"branches/{id}";

            public static string Branches => "branches";
        }

        public static class Post
        {
            public static string Branch => "branches";
        }

        protected async Task<TestServer> CreateServerAsync()
        {
            var builder = WebHost
                .CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration(b => b.AddJsonFile("appsettings.json"));

            var server = new TestServer(builder);
            await server.Host.EnsureDbContextAsync<OrganizationContext>(async (context, services) =>
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
            });
            return server;
        }
    }
}
