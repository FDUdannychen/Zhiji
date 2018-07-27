﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Infrastructure;
using Zhiji.Bills.Domain.Contracts;

namespace Zhiji.Bills.Infrastructure.Repositories
{
    class ContractRepository : RepositoryBase<ContractContext, Contract>, IContractRepository
    {
        public ContractRepository(ContractContext context)
            : base(context)
        { }

        public Task<Contract[]> ListAsync(int? customerId, int? tenementId, int? templateId)
        {
            IQueryable<Contract> query = _context.Contracts;

            if (customerId.HasValue) query = query.Where(e => e.CustomerId == customerId);
            if (tenementId.HasValue) query = query.Where(e => e.TenementId == tenementId);
            if (templateId.HasValue) query = query.Where(e => e.TemplateId == templateId);

            return query.ToArrayAsync();
        }
    }
}
