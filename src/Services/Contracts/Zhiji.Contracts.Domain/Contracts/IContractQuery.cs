using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NodaTime;
using Zhiji.Common.Domain;
using Zhiji.Common.Models;

namespace Zhiji.Contracts.Domain.Contracts
{
    public interface IContractQuery : IQuery
    {
        Task<Contract> GetAsync(int id, CancellationToken cancellationToken = default);

        Task<Contract[]> ListAsync(int? customerId, 
            int? tenementId, 
            int? templateId, 
            Range<Instant>? startDateRange, 
            Range<Instant>? endDateRange,
            CancellationToken cancellationToken = default);

        Task<Contract[]> ListEffectiveAsync(CancellationToken cancellationToken = default);
    }
}
