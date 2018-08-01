using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;
using Zhiji.Contracts.Domain.Contracts;

namespace Zhiji.Contracts.Domain.Bills
{
    public class Bill : Entity, IAggregateRoot
    {
        private string _name;
        private int _contractId;
        private Contract _contract;        
        private int _statusId;
        private BillStatus _status;

        private Bill() { }

        public Bill(string name, int contractId, BillingPeriod period)
        {
            _name = name;
            _contractId = contractId;
            this.Period = period;
            _statusId = BillStatus.Created.Id;
        }

        public string Name => _name;
        public Contract Contract => _contract;
        public BillingPeriod Period { get; private set; }
        public BillStatus Status => _status;

        public void SetPaidStatus()
        {
            if (_statusId == BillStatus.Created.Id)
            {
                _statusId = BillStatus.Paid.Id;
            }
            else
            {
                throw new ContractDomainException($"Can't change bill status from {_status.Name} to {BillStatus.Paid.Name}");
            }
        }
    }
}
