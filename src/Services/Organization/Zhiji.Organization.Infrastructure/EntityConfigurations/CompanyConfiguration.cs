﻿using System;
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
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).IsRequired().HasMaxLength(Company.NameMaxLength);

            builder.HasOne(e => e.Parent).WithMany().HasForeignKey(e => e.ParentId);
        }
    }
}