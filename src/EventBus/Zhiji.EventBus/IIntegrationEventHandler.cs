using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Zhiji.EventBus
{
    public interface IIntegrationEventHandler<T> : INotificationHandler<T>
        where T : INotification
    { }
}
