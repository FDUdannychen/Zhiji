using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Zhiji.Common.AspNetCore;
using Zhiji.Organization.Api.Commands.Branches;
using Zhiji.Organization.Api.ViewModels;

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

        [Fact]
        public async Task CreateNewBranch()
        {
            var createBranchCommand = new CreateBranchCommand
            {
                Name = "new branch"
            };

            using (var server = await CreateServerAsync())
            {
                var json = JsonConvert.SerializeObject(createBranchCommand);
                var requestContent = new StringContent(json, Encoding.UTF8, MediaTypes.ApplicationJson);
                var client = server.CreateClient();
                var response = await client.PostAsync(Post.Branch, requestContent);
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                var responseContent = await response.Content.ReadAsStringAsync();
                var vm = JsonConvert.DeserializeObject<BranchViewModel>(responseContent);
                Assert.True(vm.Id > 0);
            }
        }
    }
}
