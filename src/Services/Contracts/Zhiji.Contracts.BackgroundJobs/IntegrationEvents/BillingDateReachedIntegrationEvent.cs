using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.IntegrationEventLog;

namespace Zhiji.Contracts.BackgroundJobs.IntegrationEvents
{
    public class BillingDateReachedIntegrationEvent : IntegrationEvent
    {
        public int ContractId { get; }

        public int TemplateId { get; }

        public int CustomerId { get; }

        public int TenementId { get; }

        public DateTimeOffset BillingDate { get; }


        public BillingDateReachedIntegrationEvent(int contractId, DateTimeOffset start, DateTimeOffset end)
        {

        }
    }
}
