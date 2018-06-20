using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Organization.Domain.Departments
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<Department> GetAsync(int id);

        Department Add(Department department);

        void Update(Department department);
    }
}
