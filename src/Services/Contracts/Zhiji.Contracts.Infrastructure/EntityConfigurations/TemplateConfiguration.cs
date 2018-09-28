using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.SqlServer;
using NodaTime;
using Zhiji.Common.Domain;
using Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.Infrastructure.EntityConfigurations
{
    class TemplateConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(Template.NameMaxLength);

            builder.Property(e => e.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(e => e.BillingMode)
                .WithMany()
                .HasForeignKey(nameof(Template.BillingMode) + nameof(Entity.Id))
                .IsRequired();

            var billingDate = builder.OwnsOne(e => e.BillingDate);
            billingDate.Property(e => e.Month).IsRequired();
            billingDate.Property(e => e.Day).IsRequired();
            billingDate.Property(e => e.IntervalMonth).IsRequired();

            builder.Property(e => e.BillingPeriodMonth).IsRequired();

            builder.Property(e => e.BillingPeriodOffsetMonth).IsRequired();

            builder.Property(e => e.TimeZone)
                .IsRequired()
                .HasConversion(v => v.Id, v => DateTimeZoneProviders.Tzdb[v]);
        }
    }
}
