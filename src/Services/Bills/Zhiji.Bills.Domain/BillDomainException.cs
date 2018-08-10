using System;
using System.Collections.Generic;
using System.Text;

namespace Zhiji.Bills.Domain
{
    public class BillDomainException : Exception
    {
        public BillDomainException(string message)
            : base(message)
        { }
    }
}
