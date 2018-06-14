using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Zhiji.Common.AspNetCore;
using Zhiji.Organization.Api.Commands.Companies;
using Zhiji.Organization.Api.ViewModels;
using Zhiji.Test.Common;

namespace Zhiji.Organization.Api.Test
{
    [TestCaseOrderer("Zhiji.Test.Common.ExplicitTestCaseOrderer", "Zhiji.Test.Common")]
    public class CompanyTest : OrganizationTestBase
    {
        private async Task<HttpResponseMessage> GetCompanyImpl(int id)
        {
            var server = await TestServers.OrganizationApiServer;
            var client = server.CreateClient();
            return await client.GetAsync(Get.CompanyById(id));
        }

        private async Task<HttpResponseMessage> GetCompaniesImpl()
        {
            var server = await TestServers.OrganizationApiServer;
            var client = server.CreateClient();
            return await client.GetAsync(Get.Companies);
        }

        private async Task<HttpResponseMessage> CreateCompanyImpl(string name, int? parentId)
        {
            var command = new CreateCompanyCommand
            {
                Name = name,
                ParentId = parentId
            };

            var server = await TestServers.OrganizationApiServer;
            var json = JsonConvert.SerializeObject(command);
            var requestContent = new StringContent(json, Encoding.UTF8, MediaType.ApplicationJson);
            var client = server.CreateClient();
            return await client.PostAsync(Post.Companies, requestContent);
        }

        private async Task<T> ProjectTo<T>(HttpResponseMessage message)
        {
            var content = await message.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        [Fact, TestOrder(1)]
        public async Task GetCompanyNotFound()
        {
            var response = await GetCompanyImpl(1);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact, TestOrder(1)]
        public async Task GetCompaniesEmptyCollection()
        {
            var response = await GetCompaniesImpl();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var companies = await ProjectTo<IEnumerable<CompanyViewModel>>(response);
            Assert.False(companies.Any());
        }

        [Fact, TestOrder(2)]
        public async Task CreateCompany()
        {
            var r1 = await CreateCompanyImpl("company 1", null);
            Assert.Equal(HttpStatusCode.Created, r1.StatusCode);
            var c1 = await ProjectTo<CompanyViewModel>(r1);
            Assert.True(c1.Id > 0);

            var r2 = await CreateCompanyImpl("company 2", c1.Id);
            Assert.Equal(HttpStatusCode.Created, r2.StatusCode);
            var c2 = await ProjectTo<CompanyViewModel>(r2);
            Assert.True(c2.Id > 0);
        }

        [Fact, TestOrder(3)]
        public async Task GetCompany()
        {
            var response = await GetCompanyImpl(1);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestOrder(3)]
        public async Task GetCompanies()
        {
            var response = await GetCompaniesImpl();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var companies = JsonConvert.DeserializeObject<IEnumerable<CompanyViewModel>>(responseContent);
            Assert.Contains(companies, c => c.Parent != null);
        }
    }
}
