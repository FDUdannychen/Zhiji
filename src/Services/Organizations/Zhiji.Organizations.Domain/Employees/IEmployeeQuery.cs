using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Organizations.Domain.Employees
{
    public interface IEmployeeQuery : IQuery
    {
        Task<Employee> GetAsync(int id, CancellationToken cancellationToken = default);

        Task<Employee[]> ListByCompanyAsync(int companyId, CancellationToken cancellationToken = default);

        Task<Employee[]> ListByDepartmentAsync(int departmentId, CancellationToken cancellationToken = default);
    }
}
