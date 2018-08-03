﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Domain;
using Zhiji.Common.Infrastructure;
using Zhiji.Contracts.Domain.Bills;
using Zhiji.Contracts.Domain.Contracts;
using Zhiji.Contracts.Domain.Templates;
using Zhiji.Contracts.Infrastructure.EntityConfigurations;

namespace Zhiji.Contracts.Infrastructure
{
    public class ContractContext : DbContext, IUnitOfWork
    {
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Bill> Bills { get; set; }

        public ContractContext(DbContextOptions<ContractContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ContractConfiguration());
            builder.ApplyConfiguration(new TemplateConfiguration());
            builder.ApplyConfiguration(new EnumerationEntityConfiguration<BillingMode>());
            builder.ApplyConfiguration(new EnumerationEntityConfiguration<BillStatus>());
        }
    }
}
