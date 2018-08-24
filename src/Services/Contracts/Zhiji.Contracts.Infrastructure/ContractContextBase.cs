using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.EntityFrameworkCore;
using Zhiji.Contracts.Domain.Contracts;
using Zhiji.Contracts.Domain.Templates;
using Zhiji.Contracts.Infrastructure.EntityConfigurations;

namespace Zhiji.Contracts.Infrastructure
{
    public abstract class ContractContextBase : DbContext
    {
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<BillingMode> BillingModes { get; set; }

        public ContractContextBase(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {            
            builder.ApplyConfiguration(new ContractConfiguration());
            builder.ApplyConfiguration(new TemplateConfiguration());
            builder.ApplyConfiguration(new EnumerationEntityConfiguration<BillingMode>());
        }
    }
}
