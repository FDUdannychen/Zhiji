using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Infrastructure;
using Zhiji.Organizations.Domain.Companies;

namespace Zhiji.Organizations.Infrastructure.Repositories
{
    class CompanyRepository : RepositoryBase<OrganizationContext, Company>, ICompanyRepository
    {
        public CompanyRepository(OrganizationContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Company>> ListAsync()
        {
            return await _context.Set<Company>().ToListAsync();
        }
    }
}
