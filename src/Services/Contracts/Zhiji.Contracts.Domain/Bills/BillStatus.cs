using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;

namespace Zhiji.Contracts.Domain.Bills
{
    public class BillStatus : Enumeration
    {
        public static readonly BillStatus Created = new BillStatus(1, nameof(Created));
        public static readonly BillStatus Paid = new BillStatus(2, nameof(Paid));

        private BillStatus() { }

        private BillStatus(int id, string name)
            : base(id, name)
        { }
    }
}
