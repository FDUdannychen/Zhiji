using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Zhiji.EventBus.RabbitMQ
{
    public class RabbitMQEventBus : IEventBus
    {
        private readonly IBus _bus;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<RabbitMQEventBus> _logger;

        public RabbitMQEventBus(IBus bus, 
            IServiceScopeFactory scopeFactory,
            ILogger<RabbitMQEventBus> logger)
        {
            _bus = bus;            
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public Task PublishAsync<T>(T evt, CancellationToken cancellationToken = default)
            where T : IntegrationEvent
        {
            _logger.LogInformation($"Publishing {evt.GetType().Name} {evt.Id}");
            return _bus.PublishAsync(evt);
        }

        public void Subscribe<T>(string subscriptionId) 
            where T : IntegrationEvent
        {
            _bus.SubscribeAsync<T>(subscriptionId, async e =>
            {
                _logger.LogInformation($"New {typeof(T).Name} in subscription {subscriptionId}: {e.Id}");

                using (var scope = _scopeFactory.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediator.Publish(e);
                }
            });
        }
    }
}
