using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Zhiji.Organization.Api.Test
{
    public class BranchTest : OrganizationTestBase
    {
        [Fact]
        public async Task GetBranchReturnsNotFound()
        {
            using (var server = await CreateServerAsync())
            {
                var client = server.CreateClient();
                var result = await client.GetAsync(Get.BranchById(1));
                Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            }
        }
    }
}
