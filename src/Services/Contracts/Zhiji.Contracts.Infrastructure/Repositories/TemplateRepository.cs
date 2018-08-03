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

        public Task<Template[]> ListAsync(CancellationToken cancellationToken = default)
        {
            return _context.Templates.ToArrayAsync(cancellationToken);
        }
    }
}
