using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Infrastructure;
using Zhiji.Organizations.Domain.Employees;

namespace Zhiji.Organizations.Infrastructure.Repositories
{
    class EmployeeRepository : RepositoryBase<OrganizationContext, Employee>, IEmployeeRepository
    {
        public EmployeeRepository(OrganizationContext context)
            : base(context)
        { }

        public async Task<Employee[]> ListByCompanyAsync(int companyId)
        {
            return await _context.Employees
                .Where(e => e.Department.CompanyId == companyId)
                .ToArrayAsync();
        }

        public Task<Employee[]> ListByDepartmentAsync(int departmentId)
        {
            return _context.Employees
                .Where(e => e.DepartmentId == departmentId)
                .ToArrayAsync();
        }
    }
}
