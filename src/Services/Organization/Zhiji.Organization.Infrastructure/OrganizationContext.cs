using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Domain;
using Zhiji.Organization.Domain.Companies;
using Zhiji.Organization.Domain.Departments;
using Zhiji.Organization.Infrastructure.EntityConfigurations;

namespace Zhiji.Organization.Infrastructure
{
    public class OrganizationContext : DbContext, IUnitOfWork
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }

        public OrganizationContext(DbContextOptions<OrganizationContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CompanyConfiguration());
            builder.ApplyConfiguration(new DepartmentConfiguration());
        }
    }
}
