using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Domain;
using Zhiji.Organizations.Domain.Companies;
using Zhiji.Organizations.Domain.Departments;
using Zhiji.Organizations.Domain.Employees;
using Zhiji.Organizations.Infrastructure.EntityConfigurations;

namespace Zhiji.Organizations.Infrastructure
{
    public class OrganizationContext : DbContext, IUnitOfWork
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public OrganizationContext(DbContextOptions<OrganizationContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CompanyConfiguration());
            builder.ApplyConfiguration(new DepartmentConfiguration());
            builder.ApplyConfiguration(new EmployeeConfiguration());
        }
    }
}
