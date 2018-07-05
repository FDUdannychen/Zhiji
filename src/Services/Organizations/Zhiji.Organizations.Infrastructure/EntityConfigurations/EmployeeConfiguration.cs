using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zhiji.Organizations.Domain.Employees;

namespace Zhiji.Organizations.Infrastructure.EntityConfigurations
{
    class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).IsRequired().HasMaxLength(Employee.NameMaxLength);

            builder.HasOne(e => e.Department).WithMany().HasForeignKey(e => e.DepartmentId);
        }
    }
}
