using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Bills.Domain.Contracts
{
    public interface IContractRepository : IRepository<Contract>
    {
        Task<Contract> GetAsync(int id);

        Task<Contract[]> ListAsync(int? customerId, int? tenementId, int? templateId);

        Contract Add(Contract contract);
    }
}
