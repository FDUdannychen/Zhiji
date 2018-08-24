using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.EntityFrameworkCore;
using Zhiji.Customers.Domain.Customers;
using Zhiji.Customers.Domain.Tenements;
using Zhiji.Customers.Infrastructure.EntityConfigurations;

namespace Zhiji.Customers.Infrastructure
{
    public abstract class CustomerContextBase : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Tenement> Tenements { get; set; }
        public DbSet<TenementType> TenementTypes { get; set; }

        public CustomerContextBase(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new TenementConfiguration());
            builder.ApplyConfiguration(new EnumerationEntityConfiguration<TenementType>());
        }
    }
}
