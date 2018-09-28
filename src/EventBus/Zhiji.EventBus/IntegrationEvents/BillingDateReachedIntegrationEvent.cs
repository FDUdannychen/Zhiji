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
        public LocalDate BillingDate { get; }
        public LocalDate BillingPeriodStart { get; }
        public LocalDate BillingPeriodEnd { get; }
        public DateTimeZone TimeZone { get; }

        public BillingDateReachedIntegrationEvent(
            int contractId,
            int templateId,
            int customerId,
            int tenementId,
            LocalDate billingDate,
            LocalDate billingPeriodStart,
            LocalDate billingPeriodEnd,
            DateTimeZone timeZone)
        {
            this.ContractId = contractId;
            this.TemplateId = templateId;
            this.CustomerId = customerId;
            this.TenementId = tenementId;
            this.BillingDate = billingDate;
            this.BillingPeriodStart = billingPeriodStart;
            this.BillingPeriodEnd = billingPeriodEnd;
            this.TimeZone = timeZone;
        }
    }
}
