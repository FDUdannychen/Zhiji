using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Organizations.Domain.Employees
{
    public interface IEmployeeRepository : IRepository
    {
        Task<Employee> GetAsync(int id, CancellationToken cancellationToken = default);

        Employee Add(Employee employee);

        void Update(Employee employee);
    }
}
