using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Zhiji.Common.Models;
using Zhiji.Contracts.Domain.Contracts;

namespace Zhiji.Contracts.Infrastructure.Queries
{
    class ContractQuery : IContractQuery
    {
        private readonly ContractQueryContext _context;
        private readonly IClock _clock;

        public ContractQuery(ContractQueryContext context, IClock clock)
        {
            _context = context;
            _clock = clock;
        }

        public Task<Contract> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.Contracts
                .Include(e => e.Template.BillingMode)
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<Contract[]> ListAsync(int? customerId, 
            int? tenementId, 
            int? templateId,
            DateInterval startDateRange,
            DateInterval endDateRange,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Contract> query = _context.Contracts.Include(e => e.Template.BillingMode);

            if (customerId.HasValue) query = query.Where(e => e.CustomerId == customerId);
            if (tenementId.HasValue) query = query.Where(e => e.TenementId == tenementId);
            if (templateId.HasValue) query = query.Where(e => e.Template.Id == templateId);

            if (startDateRange != null)
            {
                query = query.Where(e => e.StartDate >= startDateRange.Start && e.StartDate <= startDateRange.End);
            }

            if (endDateRange != null)
            {
                query = query.Where(e => e.EndDate >= endDateRange.Start && e.EndDate <= endDateRange.End);
            }

            return await query.ToArrayAsync(cancellationToken);
        }

        public async Task<Contract[]> ListEffectiveAsync(CancellationToken cancellationToken = default)
        {
            var now = _clock.GetCurrentInstant();
            var today = now.InUtc().Date;

            var query = await _context.Contracts
                .Include(e => e.Template.BillingMode)
                .Where(c => c.StartDate <= today.PlusDays(1) && c.EndDate > today.PlusDays(-1))
                .ToArrayAsync(cancellationToken);

            return query.Where(c =>
            {
                var date = now.InZone(c.Template.TimeZone).Date;
                return c.StartDate <= date && c.EndDate > date;
            }).ToArray();
        }
    }
}
