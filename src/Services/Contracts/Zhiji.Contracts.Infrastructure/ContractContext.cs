﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Domain;

namespace Zhiji.Contracts.Infrastructure
{
    public class ContractContext : ContractContextBase, IUnitOfWork
    {
        public ContractContext(DbContextOptions<ContractContext> options)
            : base(options)
        { }
    }
}
