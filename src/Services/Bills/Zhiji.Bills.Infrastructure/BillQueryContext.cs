using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Zhiji.Bills.Infrastructure
{
    public class BillQueryContext : BillContextBase
    {
        public BillQueryContext(DbContextOptions<BillContext> options)
            : base(options)
        { }
    }
}
