using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Zhiji.Common.AspNetCore
{
    public static class WebHostExtensions
    {
        public static async Task<IWebHost> MigrateDbContextAsync<TContext>(this IWebHost webHost, Func<TContext, IServiceProvider, Task> seed = null)
            where TContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<TContext>())
                {
                    await context.Database.MigrateAsync();
                    if (seed != null) await seed(context, scope.ServiceProvider);
                }
            }

            return webHost;
        }
    }
}
