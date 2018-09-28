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
            DateInterval startDateRange,
            DateInterval endDateRange,
            int? billStatusId,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Bill> query = _context.Bills.Include(e => e.Status);

            if (customerId != null) query = query.Where(e => e.CustomerId == customerId.Value);
            if (tenementId != null) query = query.Where(e => e.TenementId == tenementId.Value);
            if (contractId != null) query = query.Where(e => e.Id == contractId.Value);
            if (templateId != null) query = query.Where(e => e.TemplateId == templateId.Value);
            if (billStatusId != null) query = query.Where(e => e.Status.Id == billStatusId.Value);

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
    }
}
