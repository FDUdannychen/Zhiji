using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Zhiji.Customers.Infrastructure
{
    public class CustomerQueryContext : CustomerContextBase
    {
        public CustomerQueryContext(DbContextOptions<CustomerQueryContext> options)
            : base(options)
        { }
    }
}
