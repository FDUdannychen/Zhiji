using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;
using Zhiji.Contracts.Domain.Contracts;

namespace Zhiji.Contracts.Domain.Bills
{
    public class Bill : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public Contract Contract { get; private set; }
        public BillingPeriod Period { get; private set; }
        public BillStatus Status { get; private set; }

        private int _contractId;
        private int _statusId;

        private Bill() { }

        public Bill(string name, int contractId, BillingPeriod period)
        {
            this.Name = name;
            _contractId = contractId;
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
                throw new ContractDomainException($"Can't change bill status from {this.Status.Name} to {BillStatus.Paid.Name}");
            }
        }
    }
}
