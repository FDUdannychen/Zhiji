using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.EntityFrameworkCore;
using Zhiji.Organizations.Domain.Employees;

namespace Zhiji.Organizations.Infrastructure.Repositories
{
    class EmployeeRepository : RepositoryBase<OrganizationContext, Employee>, IEmployeeRepository
    {
        public EmployeeRepository(OrganizationContext context)
            : base(context)
        { }

        public override Task<Employee> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.Employees
                .Include(e => e.Department.Company)
                .Include(e => e.Status)
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
    }
}
