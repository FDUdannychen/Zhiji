using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Zhiji.Organization.Api.Test
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
    }
}
