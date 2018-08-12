using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Contracts.Domain.Bills;

namespace Zhiji.Contracts.Infrastructure.Queries
{
    class BillQuery : IBillQuery
    {
        private readonly ContractQueryContext _context;

        public BillQuery(ContractQueryContext context)
            => _context = context;

        public Task<Bill> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.Bills
                .Include(e => e.Status)
                .Include(e => e.Contract.Template.BillingMode)                
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
            IQueryable<Bill> bills = _context.Bills
                .Include(e => e.Status)
                .Include(e => e.Contract.Template.BillingMode);

            if (customerId != null) bills = bills.Where(e => e.Contract.CustomerId == customerId.Value);
            if (tenementId != null) bills = bills.Where(e => e.Contract.TenementId == tenementId.Value);
            if (contractId != null) bills = bills.Where(e => e.Contract.Id == contractId.Value);
            if (templateId != null) bills = bills.Where(e => e.Contract.Template.Id == templateId.Value);
            if (billStatusId != null) bills = bills.Where(e => e.Status.Id == billStatusId.Value);

            return bills.ToArrayAsync(cancellationToken);
        }
    }
}
