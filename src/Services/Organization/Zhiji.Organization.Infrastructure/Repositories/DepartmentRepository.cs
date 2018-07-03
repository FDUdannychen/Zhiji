using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zhiji.Organization.Domain.Departments;

namespace Zhiji.Organization.Infrastructure.Repositories
{
    class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(OrganizationContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Department>> ListAsync(int companyId)
        {
            return await _context.Departments
                .Where(e => e.CompanyId == companyId)
                .ToListAsync();
        }
    }
}
