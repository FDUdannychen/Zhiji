using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NodaTime;

namespace Zhiji.EventBus.IntegrationEvents
{
    public class BillingDateReachedIntegrationEvent : IntegrationEvent, INotification
    {
        public int ContractId { get; }

        public Instant BillingDate { get; }

        public Instant BillingPeriodStart { get; }

        public Instant BillingPeriodEnd { get; }

        public BillingDateReachedIntegrationEvent(int contractId, Instant billingDate, Instant billingPeriodStart, Instant billingPeriodEnd)
        {
            this.ContractId = contractId;
            this.BillingDate = billingDate;
            this.BillingPeriodStart = billingPeriodStart;
            this.BillingPeriodEnd = billingPeriodEnd;
        }
    }
}
