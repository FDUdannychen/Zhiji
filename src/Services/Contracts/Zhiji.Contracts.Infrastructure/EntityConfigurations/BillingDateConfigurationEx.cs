using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.Infrastructure.EntityConfigurations
{
    static class BillingDateConfigurationEx
    {
        public static void Configure(this ReferenceOwnershipBuilder<Template, BillingDate> builder)
        {
            builder.Property(e => e.Month);
            builder.Property(e => e.Day).IsRequired();
        }
    }
}
