﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Domain;

namespace Zhiji.Customers.Infrastructure
{
    public class CustomerContext : CustomerContextBase, IUnitOfWork
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        { }
    }
}
