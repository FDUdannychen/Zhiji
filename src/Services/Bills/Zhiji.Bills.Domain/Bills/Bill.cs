using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;

namespace Zhiji.Bills.Domain.Bills
{
    public class Bill : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public int ContractId { get; private set; }
        public int CustomerId { get; private set; }
        public int TenementId { get; private set; }
        public int TemplateId { get; private set; }
        public BillPeriod Period { get; private set; }
        public BillStatus Status { get; private set; }

        private int? _statusId;

        private Bill() { }

        public Bill(string name, int contractId, int customerId, int tenementId, int templateId, BillPeriod period)
        {
            this.Name = name;
            this.ContractId = contractId;
            this.TenementId = TenementId;
            this.TemplateId = templateId;
            this.Period = period;
            _statusId = BillStatus.Created.Id;
        }

        public void SetPaidStatus()
        {
            if (_statusId == BillStatus.Created.Id)
            {
                _statusId = BillStatus.Paid.Id;
            }
            else
            {
                throw new BillDomainException($"Can't change bill status from {this.Status.Name} to {BillStatus.Paid.Name}");
            }
        }
    }
}
