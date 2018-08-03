﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Organizations.Domain.Departments
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<Department> GetAsync(int id, CancellationToken cancellationToken = default);

        Task<Department[]> ListAsync(int companyId, CancellationToken cancellationToken = default);

        Department Add(Department department);

        void Update(Department department);
    }
}
