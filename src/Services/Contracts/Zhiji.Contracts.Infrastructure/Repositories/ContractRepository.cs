using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Infrastructure;
using Zhiji.Contracts.Domain.Contracts;

namespace Zhiji.Contracts.Infrastructure.Repositories
{
    class ContractRepository : RepositoryBase<ContractContext, Contract>, IContractRepository
    {
        public ContractRepository(ContractContext context)
            : base(context)
        { }

        public override Task<Contract> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.Contracts
                .Include(e => e.Template.BillingMode)
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
    }
}
