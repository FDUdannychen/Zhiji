using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zhiji.Common.Infrastructure;
using Zhiji.Organizations.Domain.Departments;

namespace Zhiji.Organizations.Infrastructure.Repositories
{
    class DepartmentRepository : RepositoryBase<OrganizationContext, Department>, IDepartmentRepository
    {
        public DepartmentRepository(OrganizationContext context)
            : base(context)
        { }

        public override Task<Department> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context
                .Set<Department>()
                .Include(e => e.Parent)
                .Include(e => e.Company)
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public Task<Department[]> ListAsync(int companyId, CancellationToken cancellationToken = default)
        {
            return _context
                .Set<Department>()
                .Include(e => e.Parent)
                .Where(e => e.Company.Id == companyId)                
                .ToArrayAsync(cancellationToken);
        }
    }
}
