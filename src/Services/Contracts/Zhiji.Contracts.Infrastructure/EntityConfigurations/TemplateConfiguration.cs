using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.Infrastructure.EntityConfigurations
{
    class TemplateConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).IsRequired().HasMaxLength(Template.NameMaxLength);

            builder.Property(e => e.Price).IsRequired();

            var billingDate = builder.OwnsOne(e => e.BillingDate);
            billingDate.Property(e => e.Month);
            billingDate.Property(e => e.Day).IsRequired();
        }
    }
}
