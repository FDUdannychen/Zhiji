using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zhiji.EventBus
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T evt, CancellationToken cancellationToken = default)
            where T : IntegrationEvent;

        void Subscribe<T>(string subscriptionId) 
            where T : IntegrationEvent;
    }
}
