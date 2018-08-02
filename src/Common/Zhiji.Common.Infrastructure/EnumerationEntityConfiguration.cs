using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zhiji.Common.Domain;

namespace Zhiji.Common.Infrastructure
{
    public class EnumerationEntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : Enumeration
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Name).IsRequired();
        }
    }
}
