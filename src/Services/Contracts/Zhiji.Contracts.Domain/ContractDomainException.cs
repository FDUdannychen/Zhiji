using System;
using System.Collections.Generic;
using System.Text;

namespace Zhiji.Contracts.Domain
{
    public class ContractDomainException : Exception
    {
        public ContractDomainException(string message)
            : base(message)
        { }
    }
}
