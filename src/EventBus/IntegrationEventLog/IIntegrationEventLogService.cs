using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zhiji.IntegrationEventLog
{
    public interface IIntegrationEventLogService
    {
        Task SaveEventAsync(IntegrationEvent evt, CancellationToken cancellationToken = default);

        Task MarkEventPublished(IntegrationEvent evt, CancellationToken cancellationToken = default);
    }
}
