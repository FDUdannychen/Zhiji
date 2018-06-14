using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zhiji.Organization.Domain.Companies;

namespace Zhiji.Organization.Infrastructure.EntityConfigurations
{
    class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Name).IsRequired().HasMaxLength(Company.NameMaxLength);

            builder.HasOne(c => c.Parent).WithMany().HasForeignKey(c => c.ParentId);
        }
    }
}
