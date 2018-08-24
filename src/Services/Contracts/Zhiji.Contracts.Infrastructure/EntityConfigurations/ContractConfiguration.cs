using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;
using Zhiji.Common.Domain;
using Zhiji.Contracts.Domain.Contracts;

namespace Zhiji.Contracts.Infrastructure.EntityConfigurations
{
    class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.HasOne(e => e.Template)
                .WithMany()
                .HasForeignKey(nameof(Contract.Template) + nameof(Entity.Id))
                .IsRequired();

            builder.Property(e => e.CustomerId).IsRequired();
            builder.Property(e => e.TenementId).IsRequired();

            builder.Property(e => e.StartDate)
                .IsRequired()
                .HasConversion(v => v.ToDateTimeUtc(), v => Instant.FromDateTimeUtc(v));

            builder.Property(e => e.EndDate)
                .HasConversion(v => v == null ? (DateTime?)null : v.Value.ToDateTimeUtc(),
                    v => v == null ? (Instant?)null : Instant.FromDateTimeUtc(v.Value));
        }
    }
}
