using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Customers.Domain.Tenements
{
    public interface ITenementRepository : IRepository<Tenement>
    {
        Task<Tenement> GetAsync(int id);

        Task<Tenement[]> ListAsync(int ownerId);

        Tenement Add(Tenement tenement);
    }
}
