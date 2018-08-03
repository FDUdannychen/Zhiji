using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zhiji.Common.Domain;
using Zhiji.Organizations.Domain.Departments;

namespace Zhiji.Organizations.Infrastructure.EntityConfigurations
{
    class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(Department.NameMaxLength);

            builder.HasOne(e => e.Parent)
                .WithMany()
                .HasForeignKey(nameof(Department.Parent) + nameof(Entity.Id));

            builder.HasOne(e => e.Company)
                .WithMany()
                .HasForeignKey(nameof(Department.Company) + nameof(Entity.Id))
                .IsRequired();
        }
    }
}
