using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Infrastructure;
using Zhiji.Contracts.Domain.Bills;

namespace Zhiji.Contracts.Infrastructure.Repositories
{
    class BillRepository : RepositoryBase<ContractContext, Bill>, IBillRepository
    {
        public BillRepository(ContractContext context)
            : base(context)
        { }

        public override Task<Bill> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.Bills
                .Include(e => e.Status)
                .Include(e => e.Contract.Template.BillingMode)
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public Task<Bill[]> ListAsync(int contractId, CancellationToken cancellationToken = default)
            => _context.Bills.Where(e => e.Contract.Id == contractId).ToArrayAsync(cancellationToken);
    }
}
