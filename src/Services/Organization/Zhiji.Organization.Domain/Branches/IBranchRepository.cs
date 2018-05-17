using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Organization.Domain.Branches
{
    public interface IBranchRepository : IRepository<Branch>
    {
        Task<Branch> GetAsync(int id);

        Branch Add(Branch branch);

        void Update(Branch branch);
    }
}
