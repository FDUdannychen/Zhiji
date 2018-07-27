using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zhiji.Bills.Domain.Contracts;

namespace Zhiji.Bills.Infrastructure.EntityConfigurations
{
    class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.HasOne(e => e.Template).WithMany().HasForeignKey(e => e.TemplateId);

            builder.Property(e => e.CustomerId);
            builder.Property(e => e.TenementId);
            builder.Property(e => e.StartTime);
            builder.Property(e => e.EndTime);
        }
    }
}
