using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Infrastructure;
using Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.Infrastructure.Repositories
{
    class TemplateRepository : RepositoryBase<ContractContext, Template>, ITemplateRepository
    {
        public TemplateRepository(ContractContext context)
            : base(context)
        { }

        public override Task<Template> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.Templates
                .Include(e => e.BillingMode)
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public Task<Template[]> ListAsync(CancellationToken cancellationToken = default)
        {
            return _context.Templates
                .Include(e => e.BillingMode)
                .ToArrayAsync(cancellationToken);
        }
    }
}
