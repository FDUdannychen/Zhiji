using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Organization.Domain.Companies;

namespace Zhiji.Organization.Infrastructure.Repositories
{
    class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
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
