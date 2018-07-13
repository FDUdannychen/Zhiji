using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zhiji.Customers.Domain.Tenements;

namespace Zhiji.Customers.Infrastructure.EntityConfigurations
{
    class TenementConfiguration : IEntityTypeConfiguration<Tenement>
    {
        public void Configure(EntityTypeBuilder<Tenement> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.OwnsOne(e => e.Address).Configure();

            builder.HasOne(e => e.Owner).WithMany().HasForeignKey(e => e.OwnerId);
        }
    }
}
