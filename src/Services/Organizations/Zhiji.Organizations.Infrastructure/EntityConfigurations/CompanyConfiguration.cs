using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zhiji.Common.Domain;
using Zhiji.Organizations.Domain.Companies;

namespace Zhiji.Organizations.Infrastructure.EntityConfigurations
{
    class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).IsRequired().HasMaxLength(Company.NameMaxLength);

            builder.HasOne(e => e.Parent).WithMany().HasForeignKey(nameof(Company.Parent) + nameof(Entity.Id));
        }
    }
}
