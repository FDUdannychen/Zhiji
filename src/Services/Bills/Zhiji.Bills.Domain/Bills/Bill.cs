using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;
using Zhiji.Bills.Domain.Templates;

namespace Zhiji.Bills.Domain.Contracts
{
    public class Bill : Entity, IAggregateRoot
    {
        private int _templateId;
        private Template _template;
        private int _customerId;
        private int _tenementId;
        private DateTimeOffset _startTime;
        private DateTimeOffset? _endTime;

        private Contract() { }

        public Contract(int templateId, int customerId, int tenementId)
        {
            _templateId = templateId;
            _customerId = customerId;
            _tenementId = tenementId;
        }

        public Template Template => _template;
        public int TemplateId => _templateId;
        public int CustomerId => _customerId;
        public int TenementId => _tenementId;
        public DateTimeOffset StartTime => _startTime;
        public DateTimeOffset? EndTime => _endTime;
    }
}
