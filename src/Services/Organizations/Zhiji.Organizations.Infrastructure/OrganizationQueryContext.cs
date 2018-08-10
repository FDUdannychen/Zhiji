using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Zhiji.Organizations.Infrastructure
{
    public class OrganizationQueryContext : OrganizationContextBase
    {
        public OrganizationQueryContext(DbContextOptions<OrganizationQueryContext> options)
            : base(options)
        { }
    }
}
