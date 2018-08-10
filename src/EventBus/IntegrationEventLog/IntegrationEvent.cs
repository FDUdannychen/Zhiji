using System;
using System.Collections.Generic;
using System.Text;

namespace Zhiji.IntegrationEventLog
{
    public abstract class IntegrationEvent
    {
        public Guid Id { get; }

        public DateTimeOffset CreateTime { get; }

        public IntegrationEvent()
        {
            this.Id = Guid.NewGuid();
            this.CreateTime = DateTimeOffset.UtcNow;
        }
    }
}
