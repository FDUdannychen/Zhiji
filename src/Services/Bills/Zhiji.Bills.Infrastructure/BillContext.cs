using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Domain;
using Zhiji.Common.Infrastructure;
using Zhiji.Bills.Domain.Bills;
using Zhiji.Bills.Infrastructure.EntityConfigurations;

namespace Zhiji.Bills.Infrastructure
{
    public class BillContext : DbContext, IUnitOfWork
    {
        public DbSet<Bill> Bills { get; set; }

        public BillContext(DbContextOptions<BillContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BillConfiguration());
            builder.ApplyConfiguration(new EnumerationEntityConfiguration<BillStatus>());
        }
    }
}
