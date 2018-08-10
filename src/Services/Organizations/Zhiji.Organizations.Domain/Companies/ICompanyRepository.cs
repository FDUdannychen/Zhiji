using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Organizations.Domain.Companies
{
    public interface ICompanyRepository : IRepository
    {
        Task<Company> GetAsync(int id, CancellationToken cancellationToken = default);

        Company Add(Company company);

        void Update(Company company);
    }
}
