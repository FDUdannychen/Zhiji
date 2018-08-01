using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zhiji.Customers.Domain.Tenements;

namespace Zhiji.Customers.Infrastructure.EntityConfigurations
{
    class TenementTypeConfiguration : IEntityTypeConfiguration<TenementType>
    {
        public void Configure(EntityTypeBuilder<TenementType> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Name).IsRequired();
        }
    }
}
