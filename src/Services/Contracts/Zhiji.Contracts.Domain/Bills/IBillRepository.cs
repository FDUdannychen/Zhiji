using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Contracts.Domain.Bills
{
    public interface IBillRepository : IRepository<Bill>
    {
        Task<Bill[]> ListBillsAsync(int contractId);
    }
}
