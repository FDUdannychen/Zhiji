using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.Infrastructure.Queries
{
    class TemplateQuery : ITemplateQuery
    {
        private readonly ContractQueryContext _context;

        public TemplateQuery(ContractQueryContext context)
            => _context = context;

        public Task<Template> GetAsync(int id, CancellationToken cancellationToken = default)
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
