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
        public int TemplateId { get; }
        public int CustomerId { get; }
        public int TenementId { get; }
        public Instant BillingDate { get; }
        public Instant BillingPeriodStart { get; }
        public Instant BillingPeriodEnd { get; }

        public BillingDateReachedIntegrationEvent(
            int contractId,
            int templateId,
            int customerId,
            int tenementId,
            Instant billingDate, 
            Instant billingPeriodStart, 
            Instant billingPeriodEnd)
        {
            this.ContractId = contractId;
            this.TemplateId = templateId;
            this.CustomerId = customerId;
            this.TenementId = tenementId;
            this.BillingDate = billingDate;
            this.BillingPeriodStart = billingPeriodStart;
            this.BillingPeriodEnd = billingPeriodEnd;
        }
    }
}
