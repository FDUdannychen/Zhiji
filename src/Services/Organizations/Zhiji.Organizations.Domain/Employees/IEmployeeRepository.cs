using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Organizations.Domain.Employees
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> GetAsync(int id);

        Task<Employee[]> ListByCompanyAsync(int companyId);

        Task<Employee[]> ListByDepartmentAsync(int departmentId);

        Employee Add(Employee employee);

        void Update(Employee employee);
    }
}
