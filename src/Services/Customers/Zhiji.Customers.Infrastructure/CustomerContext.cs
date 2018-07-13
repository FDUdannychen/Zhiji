using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Domain;
using Zhiji.Customers.Domain.Customers;
using Zhiji.Customers.Domain.Tenements;
using Zhiji.Customers.Infrastructure.EntityConfigurations;

namespace Zhiji.Customers.Infrastructure
{
    public class CustomerContext : DbContext, IUnitOfWork
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Tenement> Tenements { get; set; }

        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new TenementConfiguration());
        }
    }
}
