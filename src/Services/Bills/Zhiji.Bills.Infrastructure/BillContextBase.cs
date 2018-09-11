using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Zhiji.Bills.Domain.Bills;
using Zhiji.Bills.Infrastructure.EntityConfigurations;
using Zhiji.Common.EntityFrameworkCore;

namespace Zhiji.Bills.Infrastructure
{
    public abstract class BillContextBase : DbContext
    {
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillStatus> BillStatuses { get; set; }

        public BillContextBase(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BillConfiguration());
            builder.ApplyConfiguration(new EnumerationEntityConfiguration<BillStatus>());
        }
    }
}
