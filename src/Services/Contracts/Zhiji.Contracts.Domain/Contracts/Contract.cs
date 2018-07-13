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
    }
}
