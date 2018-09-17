using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zhiji.Bills.Domain.Bills;
using Zhiji.EventBus;
using Zhiji.EventBus.IntegrationEvents;

namespace Zhiji.Bills.Api.IntegrationEventHandlers
{
    class BillingDateReachedIntegrationEventHandler : IIntegrationEventHandler<BillingDateReachedIntegrationEvent>
    {
        private readonly IBillRepository _billRepository;

        public BillingDateReachedIntegrationEventHandler(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public async Task Handle(BillingDateReachedIntegrationEvent evt, CancellationToken cancellationToken)
        {
            var bill = new Bill(evt.ContractId, evt.TemplateId, evt.CustomerId, evt.TenementId, evt.BillingPeriodStart, evt.BillingPeriodEnd);
            _billRepository.Add(bill);
            await _billRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
