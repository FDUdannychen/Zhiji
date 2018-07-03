using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Organization.Domain.Employees;

namespace Zhiji.Organization.Infrastructure.Repositories
{
    class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(OrganizationContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Employee>> ListByCompanyAsync(int companyId)
        {
            return await _context.Employees
                .Where(e => e.Department.CompanyId == companyId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> ListByDepartmentAsync(int departmentId)
        {
            return await _context.Employees
                .Where(e => e.DepartmentId == departmentId)
                .ToListAsync();
        }
    }
}
