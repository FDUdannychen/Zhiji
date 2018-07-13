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

        public Task<Department[]> ListAsync(int companyId)
        {
            return _context.Departments
                .Where(e => e.CompanyId == companyId)
                .ToArrayAsync();
        }
    }
}
