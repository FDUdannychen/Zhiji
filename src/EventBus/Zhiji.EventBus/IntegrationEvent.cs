using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using NodaTime;

namespace Zhiji.EventBus
{
    public abstract class IntegrationEvent : INotification
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
