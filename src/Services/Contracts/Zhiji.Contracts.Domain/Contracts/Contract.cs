using System;
using System.Collections.Generic;
using System.Text;
using NodaTime;
using Zhiji.Common.Domain;
using Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.Domain.Contracts
{
    public class Contract : Entity, IAggregateRoot
    {
        public Template Template { get; private set; }
        public int CustomerId { get; private set; }
        public int TenementId { get; private set; }
        public Instant StartDate { get; set; }
        public Instant? EndDate { get; set; }

        private int? _templateId;

        private Contract() { }

        public Contract(int templateId, int customerId, int tenementId, Instant startDate, Instant? endDate)
        {
            _templateId = templateId;
            this.CustomerId = customerId;
            this.TenementId = tenementId;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }
}
