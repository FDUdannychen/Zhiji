using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Bills.Domain.Bills
{
    public interface IBillRepository : IRepository<Bill>
    {
        Task<Bill> GetAsync(int id, CancellationToken cancellationToken = default);

        Bill Add(Bill bill);

        Task<Bill[]> ListAsync(int? customerId = null, int? tenementId = null, int? contractId = null, int? templateId = null, int? billStatusId = null, CancellationToken cancellationToken = default);
    }
}
