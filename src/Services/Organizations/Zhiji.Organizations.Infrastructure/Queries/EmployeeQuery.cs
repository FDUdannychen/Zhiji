using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Organizations.Domain.Employees;

namespace Zhiji.Organizations.Infrastructure.Queries
{
    class EmployeeQuery : IEmployeeQuery
    {
        private readonly OrganizationQueryContext _context;

        public EmployeeQuery(OrganizationQueryContext context)
            => _context = context;

        public Task<Employee> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.Employees
                .Include(e => e.Department.Company)
                .Include(e => e.Status)
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public Task<Employee[]> ListByCompanyAsync(int companyId, CancellationToken cancellationToken = default)
        {
            return _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Status)
                .Where(e => e.Department.Company.Id == companyId)
                .ToArrayAsync(cancellationToken);
        }

        public Task<Employee[]> ListByDepartmentAsync(int departmentId, CancellationToken cancellationToken = default)
        {
            return _context.Employees
                .Include(e => e.Status)
                .Where(e => e.Department.Id == departmentId)
                .ToArrayAsync(cancellationToken);
        }
    }
}
