using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Bills.Domain.Bills;
using Zhiji.Common.EntityFrameworkCore;

namespace Zhiji.Bills.Infrastructure.Repositories
{
    class BillRepository : RepositoryBase<BillContext, Bill>, IBillRepository
    {
        public BillRepository(BillContext context)
            : base(context)
        { }

        public override Task<Bill> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.Bills
                .Include(e => e.Status)
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public Task<Bill[]> ListAsync(
            int? customerId = null, 
            int? tenementId = null, 
            int? contractId = null, 
            int? templateId = null, 
            int? billStatusId = null, 
            CancellationToken cancellationToken = default)
        {
            IQueryable<Bill> bills = _context.Bills.Include(e => e.Status);

            if (customerId != null) bills = bills.Where(e => e.CustomerId == customerId.Value);
            if (tenementId != null) bills = bills.Where(e => e.TenementId == tenementId.Value);
            if (contractId != null) bills = bills.Where(e => e.Id == contractId.Value);
            if (templateId != null) bills = bills.Where(e => e.TemplateId == templateId.Value);
            if (billStatusId != null) bills = bills.Where(e => e.Status.Id == billStatusId.Value);

            return bills.ToArrayAsync(cancellationToken);
        }
    }
}
