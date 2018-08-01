using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Contracts.Domain.Contracts
{
    public interface IContractRepository : IRepository<Contract>
    {
        Task<Contract> GetAsync(int id);

        Task<Contract[]> ListAsync(int? customerId, int? tenementId, int? templateId);

        Task<Contract[]> ListEffectiveAsync();

        Contract Add(Contract contract);
    }
}
