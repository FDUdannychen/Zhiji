using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zhiji.EventBus;
using Zhiji.EventBus.IntegrationEvents;

namespace Zhiji.Bills.Api.IntegrationEventHandlers
{
    class BillingDateReachedIntegrationEventHandler : IIntegrationEventHandler<BillingDateReachedIntegrationEvent>
    {
        public Task Handle(BillingDateReachedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
