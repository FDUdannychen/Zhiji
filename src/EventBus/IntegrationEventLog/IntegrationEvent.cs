using System;
using System.Collections.Generic;
using System.Text;
using NodaTime;

namespace Zhiji.IntegrationEventLog
{
    public abstract class IntegrationEvent
    {
        public Guid Id { get; protected set; }

        public Instant CreateTime { get; protected set; }

        public IntegrationEvent()
        {
            this.Id = Guid.NewGuid();
            this.CreateTime = SystemClock.Instance.GetCurrentInstant();
        }
    }
}
