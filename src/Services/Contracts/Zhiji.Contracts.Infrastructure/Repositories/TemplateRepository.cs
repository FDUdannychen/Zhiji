using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.EntityFrameworkCore;
using Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.Infrastructure.Repositories
{
    class TemplateRepository : RepositoryBase<ContractContext, Template>, ITemplateRepository
    {
        public TemplateRepository(ContractContext context)
            : base(context)
        { }
    }
}
