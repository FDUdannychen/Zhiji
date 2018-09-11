using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Zhiji.EventBus.RabbitMQ
{
    public class RabbitMQEventBus : IEventBus
    {
        private readonly IBus _bus;        
        private readonly IServiceScopeFactory _scopeFactory;

        public RabbitMQEventBus(IBus bus, IServiceScopeFactory scopeFactory)
        {
            _bus = bus;            
            _scopeFactory = scopeFactory;
        }

        public Task PublishAsync<T>(T evt, CancellationToken cancellationToken = default)
            where T : IntegrationEvent
        {
            return _bus.PublishAsync(evt);
        }

        public void Subscribe<T>(string subscriptionId) where T : class, INotification
        {
            _bus.SubscribeAsync<T>(subscriptionId, e =>
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    return mediator.Publish(e);
                }
            });
        }
    }
}
