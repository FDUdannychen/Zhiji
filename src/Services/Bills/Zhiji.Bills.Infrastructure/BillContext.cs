﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Domain;

namespace Zhiji.Bills.Infrastructure
{
    public class BillContext : BillContextBase, IUnitOfWork
    {
        public BillContext(DbContextOptions<BillContext> options)
            : base(options)
        { }
    }
}
