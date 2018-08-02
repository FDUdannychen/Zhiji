using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;
using Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.Domain.Contracts
{
    public class Contract : Entity, IAggregateRoot
    {
        public Template Template { get; private set; }
        public int CustomerId { get; private set; }
        public int TenementId { get; private set; }
        public DateTimeOffset StartTime { get; private set; }
        public DateTimeOffset? EndTime { get; private set; }

        private int _templateId;

        private Contract() { }

        public Contract(int templateId, int customerId, int tenementId)
            : this()
        {
            _templateId = templateId;
            this.CustomerId = customerId;
            this.TenementId = tenementId;
        }


    }
}
