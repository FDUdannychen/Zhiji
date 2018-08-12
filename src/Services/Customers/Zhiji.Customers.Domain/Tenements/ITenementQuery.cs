using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Customers.Domain.Tenements
{
    public interface ITenementQuery : IQuery
    {
        Task<Tenement> GetAsync(int id, CancellationToken cancellationToken = default);

        Task<Tenement[]> ListAsync(int ownerId, CancellationToken cancellationToken = default);
    }
}
