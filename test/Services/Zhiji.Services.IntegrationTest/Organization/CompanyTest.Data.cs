using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Zhiji.Organization.Domain.Companies;

namespace Zhiji.Services.IntegrationTest.Organization
{
    public partial class CompanyTest
    {
        static string MaxLengthCompanyName = new string(Enumerable.Repeat('a', Company.NameMaxLength).ToArray());
        static string[] InvalidCompanyNames = new[] { null, string.Empty, MaxLengthCompanyName + 'a' };
        static int?[] InvalidParentIds = new int?[] { -1, 0 };

        public static IEnumerable<object[]> CreateCompanyData
        {
            get
            {
                foreach (var invalidName in InvalidCompanyNames)
                {
                    yield return new object[] { invalidName, null, HttpStatusCode.BadRequest };
                }

                foreach (var invalidParentId in InvalidParentIds)
                {
                    yield return new object[] { "a", invalidParentId, HttpStatusCode.BadRequest };
                }

                yield return new object[] { "a", null, HttpStatusCode.Created };
                yield return new object[] { MaxLengthCompanyName, null, HttpStatusCode.Created };
                yield return new object[] { "b", 1, HttpStatusCode.Created };
            }
        }
    }
}
