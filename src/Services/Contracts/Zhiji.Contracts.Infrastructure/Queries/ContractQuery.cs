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
            Range<Instant>? startDateRange,
            Range<Instant>? endDateRange,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Contract> query = _context.Contracts.Include(e => e.Template.BillingMode);

            if (customerId.HasValue) query = query.Where(e => e.CustomerId == customerId);
            if (tenementId.HasValue) query = query.Where(e => e.TenementId == tenementId);
            if (templateId.HasValue) query = query.Where(e => e.Template.Id == templateId);

            if (startDateRange.HasValue)
            {
                if (startDateRange.Value.LowerBound.HasValue)
                {
                    var lowerBound = startDateRange.Value.LowerBound.Value;
                    query = startDateRange.Value.IncludeLowerBound
                        ? query.Where(e => e.Start >= lowerBound)
                        : query.Where(e => e.Start > lowerBound);
                }

                if (startDateRange.Value.UpperBound.HasValue)
                {
                    var upperBound = startDateRange.Value.UpperBound.Value;
                    query = startDateRange.Value.IncludeUpperBound
                        ? query.Where(e => e.Start <= upperBound)
                        : query.Where(e => e.Start < upperBound);
                }
            }

            if (endDateRange.HasValue)
            {
                if (endDateRange.Value.LowerBound.HasValue)
                {
                    var lowerBound = startDateRange.Value.LowerBound.Value;
                    query = endDateRange.Value.IncludeLowerBound
                        ? query.Where(e => e.End >= lowerBound)
                        : query.Where(e => e.End > lowerBound);
                }

                if (endDateRange.Value.UpperBound.HasValue)
                {
                    var upperBound = endDateRange.Value.UpperBound.Value;
                    query = endDateRange.Value.IncludeUpperBound
                        ? query.Where(e => e.End <= upperBound)
                        : query.Where(e => e.End < upperBound);
                }
            }

            return await query.ToArrayAsync(cancellationToken);
        }

        public async Task<Contract[]> ListEffectiveAsync(CancellationToken cancellationToken = default)
        {
            var now = _clock.GetCurrentInstant();

            return await _context.Contracts
                .Include(e => e.Template.BillingMode)
                .Where(c => c.Start <= now && c.End > now)
                .ToArrayAsync(cancellationToken);
        }
    }
}
