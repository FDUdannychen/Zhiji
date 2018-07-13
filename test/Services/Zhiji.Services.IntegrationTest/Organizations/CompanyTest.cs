using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Zhiji.Organizations.Api.Models.Companies;
using Zhiji.Test.Common;

namespace Zhiji.Services.IntegrationTest.Organization
{
    [TestCaseOrderer("Zhiji.Test.Common.ExplicitTestCaseOrderer", "Zhiji.Test.Common")]
    public partial class CompanyTest : OrganizationTestBase
    {
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

            var companies = await ProjectTo<ViewCompany[]>(response);
            Assert.False(companies.Any());
        }

        [Theory, TestOrder(2)]
        [MemberData(nameof(CreateCompanyData))]
        public async Task CreateCompany(string name, int? parentId, HttpStatusCode expected)
        {
            var response = await CreateCompanyImpl(name, parentId);
            Assert.Equal(expected, response.StatusCode);
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
            var companies = JsonConvert.DeserializeObject<ViewCompany[]>(responseContent);
            Assert.Contains(companies, c => c.Parent != null && companies.Any(p => p.Id == c.Parent.Id));
        }
    }
}
