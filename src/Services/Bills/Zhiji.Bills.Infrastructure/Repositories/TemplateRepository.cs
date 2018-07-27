using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Infrastructure;
using Zhiji.Bills.Domain.Templates;

namespace Zhiji.Bills.Infrastructure.Repositories
{
    class TemplateRepository : RepositoryBase<ContractContext, Template>, ITemplateRepository
    {
        public TemplateRepository(ContractContext context)
            : base(context)
        { }

        public Task<Template[]> ListAsync()
        {
            return _context.Templates.ToArrayAsync();
        }
    }
}
