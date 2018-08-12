using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Zhiji.Contracts.Infrastructure
{
    public class ContractQueryContext : ContractContextBase
    {
        public ContractQueryContext(DbContextOptions<ContractQueryContext> options)
            : base(options)
        { }
    }
}
