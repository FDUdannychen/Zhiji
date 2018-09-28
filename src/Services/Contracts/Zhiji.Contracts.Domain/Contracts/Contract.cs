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
        public LocalDate StartDate { get; set; }
        public LocalDate? EndDate { get; set; }

        private int? _templateId;

        private Contract() { }

        public Contract(int templateId, int customerId, int tenementId, LocalDate start, LocalDate? end)
        {
            _templateId = templateId;

            this.CustomerId = customerId;
            this.TenementId = tenementId;
            this.StartDate = start;
            this.EndDate = end;
        }
    }
}
