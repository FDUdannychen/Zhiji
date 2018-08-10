using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Organizations.Domain.Departments
{
    public interface IDepartmentQuery : IQuery
    {
        Task<Department> GetAsync(int id, CancellationToken cancellationToken = default);

        Task<Department[]> ListAsync(int companyId, CancellationToken cancellationToken = default);
    }
}
