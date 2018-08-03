using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Contracts.Domain.Bills
{
    public interface IBillRepository : IRepository<Bill>
    {
        Bill Add(Bill bill);

        Task<Bill[]> ListAsync(int contractId, CancellationToken cancellationToken = default);
    }
}
