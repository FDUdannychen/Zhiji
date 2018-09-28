using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NodaTime;
using Zhiji.Common.Domain;
using Zhiji.Common.Models;

namespace Zhiji.Bills.Domain.Bills
{
    public interface IBillQuery : IQuery
    {
        Task<Bill[]> ListAsync(int? customerId,
            int? tenementId,
            int? contractId,
            int? templateId,
            DateInterval startDateRange,
            DateInterval endDateRange,
            int? billStatusId,
            CancellationToken cancellationToken = default);
    }
}
