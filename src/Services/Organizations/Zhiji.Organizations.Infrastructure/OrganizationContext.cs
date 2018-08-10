using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Domain;

namespace Zhiji.Organizations.Infrastructure
{
    public class OrganizationContext : OrganizationContextBase, IUnitOfWork
    {
        public OrganizationContext(DbContextOptions<OrganizationContext> options)
            : base(options)
        { }
    }
}
