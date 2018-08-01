using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;

namespace Zhiji.Contracts.Domain.Templates
{
    public class BillingMode : Enumeration
    {
        public static readonly BillingMode Year = new BillingMode(1, nameof(Year));
        public static readonly BillingMode Quarter = new BillingMode(2, nameof(Quarter));
        public static readonly BillingMode Month = new BillingMode(3, nameof(Month));

        private BillingMode() { }

        private BillingMode(int id, string name)
            : base(id, name)
        { }
    }
}
