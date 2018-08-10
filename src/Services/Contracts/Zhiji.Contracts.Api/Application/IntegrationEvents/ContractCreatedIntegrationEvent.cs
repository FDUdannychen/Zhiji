using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.IntegrationEventLog;

namespace Zhiji.Contracts.Api.Application.IntegrationEvents
{
    public class ContractCreatedIntegrationEvent : IntegrationEvent
    {
        public int ContractId { get; }

        public int CustomerId { get; }

        public int TenementId { get; }

        public int TemplateId { get; }

        public ContractCreatedIntegrationEvent(
            int contractId, 
            int customerId, 
            int tenementId, 
            int templateId)
        {
            this.ContractId = contractId;
            this.CustomerId = customerId;
        }
    }
}
