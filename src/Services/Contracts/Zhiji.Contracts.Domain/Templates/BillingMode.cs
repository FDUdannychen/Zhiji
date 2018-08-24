using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;

namespace Zhiji.Contracts.Domain.Templates
{
    public class BillingMode : Enumeration
    {
        public static readonly BillingMode Prepaid = new BillingMode(1, nameof(Prepaid));
        public static readonly BillingMode Postpaid = new BillingMode(2, nameof(Postpaid));

        private BillingMode() { }

        private BillingMode(int id, string name)
            : base(id, name)
        { }
    }
}
