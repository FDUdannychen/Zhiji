using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Organizations.Domain.Companies;

namespace Zhiji.Organizations.Infrastructure.Queries
{
    class CompanyQuery : ICompanyQuery
    {
        private readonly OrganizationQueryContext _context;

        public CompanyQuery(OrganizationQueryContext context)
            => _context = context;

        public Task<Company> GetAsync(int id, CancellationToken cancellationToken = default)
            => _context.Companies.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

        public Task<Company[]> ListAsync(CancellationToken cancellationToken = default) 
            => _context.Companies.ToArrayAsync(cancellationToken);
    }
}
