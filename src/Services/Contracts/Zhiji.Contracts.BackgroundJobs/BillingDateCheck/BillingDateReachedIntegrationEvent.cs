using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.IntegrationEventLog;

namespace Zhiji.Contracts.BackgroundJobs.BillingDateCheck
{
    public class BillingDateReachedIntegrationEvent : IntegrationEvent
    {
        public int ContractId { get; }

        public DateTimeOffset BillingDate { get; }

        public DateTimeOffset BillingPeriodStart { get; }

        public DateTimeOffset BillingPeriodEnd { get; }

        public BillingDateReachedIntegrationEvent(int contractId, DateTimeOffset billingDate, DateTimeOffset billingPeriodStart, DateTimeOffset billingPeriodEnd)
        {
            this.ContractId = contractId;
            this.BillingDate = billingDate;
            this.BillingPeriodStart = billingPeriodStart;
            this.BillingPeriodEnd = billingPeriodEnd;
        }
    }
}
