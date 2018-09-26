using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Zhiji.Bills.Domain.Bills;
using Zhiji.Common.Models;

namespace Zhiji.Bills.Infrastructure.Queries
{
    class BillQuery : IBillQuery
    {
        private readonly BillQueryContext _context;

        public BillQuery(BillQueryContext context)
        {
            _context = context;
        }

        public async Task<Bill[]> ListAsync(int? customerId,
            int? tenementId,
            int? contractId,
            int? templateId,
            Range<Instant>? startDateRange,
            Range<Instant>? endDateRange,
            int? billStatusId,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Bill> query = _context.Bills.Include(e => e.Status);

            if (customerId != null) query = query.Where(e => e.CustomerId == customerId.Value);
            if (tenementId != null) query = query.Where(e => e.TenementId == tenementId.Value);
            if (contractId != null) query = query.Where(e => e.Id == contractId.Value);
            if (templateId != null) query = query.Where(e => e.TemplateId == templateId.Value);
            if (billStatusId != null) query = query.Where(e => e.Status.Id == billStatusId.Value);

            if (startDateRange.HasValue)
            {
                if (startDateRange.Value.LowerBound.HasValue)
                {
                    var lowerBound = startDateRange.Value.LowerBound.Value;
                    query = startDateRange.Value.IncludeLowerBound
                        ? query.Where(e => e.StartDate >= lowerBound)
                        : query.Where(e => e.StartDate > lowerBound);
                }

                if (startDateRange.Value.UpperBound.HasValue)
                {
                    var upperBound = startDateRange.Value.UpperBound.Value;
                    query = startDateRange.Value.IncludeUpperBound
                        ? query.Where(e => e.StartDate <= upperBound)
                        : query.Where(e => e.StartDate < upperBound);
                }
            }

            if (endDateRange.HasValue)
            {
                if (endDateRange.Value.LowerBound.HasValue)
                {
                    var lowerBound = startDateRange.Value.LowerBound.Value;
                    query = endDateRange.Value.IncludeLowerBound
                        ? query.Where(e => e.EndDate >= lowerBound)
                        : query.Where(e => e.EndDate > lowerBound);
                }

                if (endDateRange.Value.UpperBound.HasValue)
                {
                    var upperBound = endDateRange.Value.UpperBound.Value;
                    query = endDateRange.Value.IncludeUpperBound
                        ? query.Where(e => e.EndDate <= upperBound)
                        : query.Where(e => e.EndDate < upperBound);
                }
            }

            return await query.ToArrayAsync(cancellationToken);
        }
    }
}
