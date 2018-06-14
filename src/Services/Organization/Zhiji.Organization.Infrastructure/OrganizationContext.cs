using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Zhiji.Organization.Domain.Companies;
using Zhiji.Organization.Infrastructure.EntityConfigurations;
using Zhiji.Common.Domain;

namespace Zhiji.Organization.Infrastructure
{
    public class OrganizationContext : DbContext, IUnitOfWork
    {
        public DbSet<Company> Companies { get; set; }

        public OrganizationContext(DbContextOptions<OrganizationContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CompanyConfiguration());
        }
    }
}
