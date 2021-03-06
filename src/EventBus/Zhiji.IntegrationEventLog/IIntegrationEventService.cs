﻿using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using NodaTime;
using Zhiji.EventBus;

namespace Zhiji.IntegrationEventLog
{
    public interface IIntegrationEventService
    {
        Task SaveEventAsync(IntegrationEvent evt, DbTransaction transaction = default, CancellationToken cancellationToken = default);

        Task MarkEventPublishedAsync(IntegrationEvent evt, CancellationToken cancellationToken = default);

        Task<Instant> GetLastPublishTimeAsync<T>(CancellationToken cancellationToken = default) where T : IntegrationEvent;
    }
}
