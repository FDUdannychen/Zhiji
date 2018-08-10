using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zhiji.Common.Domain;
using Zhiji.Bills.Domain.Bills;

namespace Zhiji.Bills.Infrastructure.EntityConfigurations
{
    class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.ContractId).IsRequired();
            builder.Property(e => e.CustomerId).IsRequired();
            builder.Property(e => e.TenementId).IsRequired();
            builder.Property(e => e.TemplateId).IsRequired();

            var billingPeriod = builder.OwnsOne(e => e.Period);
            billingPeriod.Property(e => e.Start).IsRequired();
            billingPeriod.Property(e => e.End).IsRequired();

            builder.HasOne(e => e.Status)
                .WithMany()
                .HasForeignKey(nameof(Bill.Status) + nameof(Entity.Id))
                .IsRequired();
        }
    }
}
