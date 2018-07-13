using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zhiji.Customers.Domain;

namespace Zhiji.Customers.Infrastructure.EntityConfigurations
{
    static class AddressConfigurationEx
    {
        public static void Configure<TEntity>(this ReferenceOwnershipBuilder<TEntity, Address> builder)
            where TEntity : class
        {
            builder.Property(e => e.Country).IsRequired().HasMaxLength(Address.CountryMaxLength);
            builder.Property(e => e.Province).IsRequired().HasMaxLength(Address.ProvinceMaxLength);
            builder.Property(e => e.City).IsRequired().HasMaxLength(Address.CityMaxLength);
            builder.Property(e => e.Street).IsRequired().HasMaxLength(Address.StreetMaxLength);
        }
    }
}
