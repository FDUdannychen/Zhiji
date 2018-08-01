using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;
using Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.Domain.Contracts
{
    public class Contract : Entity, IAggregateRoot
    {
        private int _templateId;
        private Template _template;
        private int _customerId;
        private int _tenementId;
        private DateTimeOffset _startTime;
        private DateTimeOffset? _endTime;

        private Contract() { }

        public Contract(int templateId, int customerId, int tenementId)
            : this()
        {
            _templateId = templateId;
            _customerId = customerId;
            _tenementId = tenementId;
        }

        public Template Template => _template;
        public int CustomerId => _customerId;
        public int TenementId => _tenementId;
        public DateTimeOffset StartTime => _startTime;
        public DateTimeOffset? EndTime => _endTime;
    }
}
