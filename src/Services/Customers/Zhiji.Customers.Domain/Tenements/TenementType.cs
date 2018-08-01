using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;

namespace Zhiji.Customers.Domain.Tenements
{
    public class TenementType : Enumeration
    {
        public static readonly TenementType Residence = new TenementType(1, nameof(Residence));
        public static readonly TenementType NonResidence = new TenementType(2, nameof(NonResidence));

        private TenementType() { }

        private TenementType(int id, string name)
            : base(id, name)
        { }
    }
}
