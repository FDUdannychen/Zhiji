using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Zhiji.EventBus;

namespace Zhiji.IntegrationEventLog
{
    public class IntegrationEventService : IIntegrationEventService
    {
        private readonly IntegrationEventContext _context;

        public IntegrationEventService(DbConnection connection)
        {
            _context = new IntegrationEventContext(
                new DbContextOptionsBuilder<IntegrationEventContext>()
                    .UseSqlServer(connection)
                    .Options);
        }

        public async Task SaveEventAsync(IntegrationEvent evt, DbTransaction transaction = default, CancellationToken cancellationToken = default)
        {
            var entry = new IntegrationEventEntry(evt);
            if (transaction != null) _context.Database.UseTransaction(transaction);
            _context.IntegrationEvents.Add(entry);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task MarkEventPublishedAsync(IntegrationEvent evt, CancellationToken cancellationToken = default)
        {
            var entry = await _context.IntegrationEvents.SingleAsync(e => e.Id == evt.Id, cancellationToken);
            entry.PublishTimes++;
            entry.Status = IntegrationEventStatus.Published;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Instant> GetLastCreateTimeAsync<T>(CancellationToken cancellationToken = default) where T : IntegrationEvent
        {
            var latest = await _context.IntegrationEvents
                .Where(e => e.Type == typeof(T).Name)
                .OrderByDescending(e => e.CreateTime)
                .FirstOrDefaultAsync(cancellationToken);

            if (latest is null) return Instant.MinValue;
            return latest.CreateTime;
        }
    }
}
