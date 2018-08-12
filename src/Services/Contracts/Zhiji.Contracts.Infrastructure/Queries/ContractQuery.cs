using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Contracts.Domain.Contracts;

namespace Zhiji.Contracts.Infrastructure.Queries
{
    class ContractQuery : IContractQuery
    {
        private readonly ContractQueryContext _context;

        public ContractQuery(ContractQueryContext context)
            => _context = context;

        public Task<Contract> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.Contracts
                .Include(e => e.Template.BillingMode)
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public Task<Contract[]> ListAsync(int? customerId, int? tenementId, int? templateId, CancellationToken cancellationToken = default)
        {
            IQueryable<Contract> query = _context.Contracts.Include(e => e.Template.BillingMode);

            if (customerId.HasValue) query = query.Where(e => e.CustomerId == customerId);
            if (tenementId.HasValue) query = query.Where(e => e.TenementId == tenementId);
            if (templateId.HasValue) query = query.Where(e => e.Template.Id == templateId);

            return query.ToArrayAsync(cancellationToken);
        }

        public Task<Contract[]> ListEffectiveAsync(CancellationToken cancellationToken = default)
        {
            return _context.Contracts
                .Include(e => e.Template.BillingMode)
                .Where(c => c.StartTime <= DateTimeOffset.UtcNow && c.EndTime > DateTimeOffset.UtcNow)
                .ToArrayAsync(cancellationToken);
        }
    }
}
