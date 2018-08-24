using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NodaTime;

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

        public async Task SaveEventAsync(IntegrationEvent evt, DbTransaction transaction, CancellationToken cancellationToken = default)
        {
            var entry = new IntegrationEventEntry(evt);
            _context.Database.UseTransaction(transaction);
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

        public Task<Instant> GetLastCreateTimeAsync<T>(CancellationToken cancellationToken = default) where T : IntegrationEvent
        {
            return _context
                .IntegrationEvents
                .Where(e => e.Type == typeof(T).Name)                
                .MaxAsync(e => e.CreateTime, cancellationToken);
        }
    }
}
