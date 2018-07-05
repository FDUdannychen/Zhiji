using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Zhiji.Common.AspNetCore;
using Zhiji.Organizations.Api.Models;

namespace Zhiji.Services.IntegrationTest.Organization
{
    public abstract class OrganizationTestBase
    {
        public static class Get
        {
            public static string CompanyById(int id) => $"companies/{id}";

            public static string Companies => "companies";
        }

        public static class Post
        {
            public static string Companies => "companies";
        }

        protected async Task<HttpResponseMessage> GetCompanyImpl(int id)
        {
            var server = await TestServers.OrganizationApiServer;
            var client = server.CreateClient();
            return await client.GetAsync(Get.CompanyById(id));
        }

        protected async Task<HttpResponseMessage> GetCompaniesImpl()
        {
            var server = await TestServers.OrganizationApiServer;
            var client = server.CreateClient();
            return await client.GetAsync(Get.Companies);
        }

        protected async Task<HttpResponseMessage> CreateCompanyImpl(string name, int? parentId)
        {
            var command = new CreateCompany
            {
                Name = name,
                ParentId = parentId
            };

            var server = await TestServers.OrganizationApiServer;
            var json = JsonConvert.SerializeObject(command);
            var requestContent = new StringContent(json, Encoding.UTF8, ContentType.ApplicationJson);
            var client = server.CreateClient();
            return await client.PostAsync(Post.Companies, requestContent);
        }

        protected async Task<T> ProjectTo<T>(HttpResponseMessage message)
        {
            var content = await message.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
