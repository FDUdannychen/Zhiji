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
    }
}
