using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;
using Zhiji.Common.AspNetCore;
using Zhiji.Organization.Api.Commands.Branches;
using Zhiji.Organization.Api.ViewModels;
using Zhiji.Test.Common;

namespace Zhiji.Organization.Api.Test
{
    [TestCaseOrderer("Zhiji.Test.Common.ExplicitTestCaseOrderer", "Zhiji.Test.Common")]
    public class BranchTest : OrganizationTestBase
    {
        private async Task<HttpResponseMessage> GetBranchImpl(int id)
        {
            var server = await TestServers.OrganizationApiServer;
            var client = server.CreateClient();
            return await client.GetAsync(Get.BranchById(id));
        }

        private async Task<HttpResponseMessage> GetBranchesImpl()
        {
            var server = await TestServers.OrganizationApiServer;
            var client = server.CreateClient();
            return await client.GetAsync(Get.Branches);
        }

        private async Task<T> ProjectTo<T>(HttpResponseMessage message)
        {
            var content = await message.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        [Fact, TestOrder(1)]
        public async Task GetBranchReturnsNotFound()
        {
            var response = await GetBranchImpl(1);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact, TestOrder(1)]        
        public async Task GetBranchesReturnsEmptyCollection()
        {
            var response = await GetBranchesImpl();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var branches = await ProjectTo<IEnumerable<BranchViewModel>>(response);
            Assert.False(branches.Any());
        }

        [Fact, TestOrder(2)]
        public async Task CreateNewBranch()
        {
            var createBranchCommand = new CreateBranchCommand
            {
                Name = "new branch"
            };

            var server = await TestServers.OrganizationApiServer;
            var json = JsonConvert.SerializeObject(createBranchCommand);
            var requestContent = new StringContent(json, Encoding.UTF8, MediaType.ApplicationJson);
            var client = server.CreateClient();
            var response = await client.PostAsync(Post.Branch, requestContent);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var vm = JsonConvert.DeserializeObject<BranchViewModel>(responseContent);
            Assert.True(vm.Id > 0);
        }

        [Fact, TestOrder(3)]
        public async Task GetBranch()
        {
            var response = await GetBranchImpl(1);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestOrder(3)]
        public async Task GetBranches()
        {
            var response = await GetBranchesImpl();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var vm = JsonConvert.DeserializeObject<IEnumerable<BranchViewModel>>(responseContent);
            Assert.True(vm.Any());
        }
    }
}
