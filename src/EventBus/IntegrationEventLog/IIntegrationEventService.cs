using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using NodaTime;

namespace Zhiji.IntegrationEventLog
{
    public interface IIntegrationEventService
    {
        Task SaveEventAsync(IntegrationEvent evt, DbTransaction transaction, CancellationToken cancellationToken = default);

        Task MarkEventPublishedAsync(IntegrationEvent evt, CancellationToken cancellationToken = default);

        Task<Instant> GetLastCreateTimeAsync<T>(CancellationToken cancellationToken = default) where T : IntegrationEvent;
    }
}
