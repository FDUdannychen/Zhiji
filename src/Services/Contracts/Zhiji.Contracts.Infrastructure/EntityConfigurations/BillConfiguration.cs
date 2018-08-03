using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zhiji.Common.Domain;
using Zhiji.Contracts.Domain.Bills;

namespace Zhiji.Contracts.Infrastructure.EntityConfigurations
{
    class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).IsRequired();

            builder.HasOne(e => e.Contract)
                .WithMany()
                .HasForeignKey(nameof(Bill.Contract) + nameof(Entity.Id))
                .IsRequired();

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
