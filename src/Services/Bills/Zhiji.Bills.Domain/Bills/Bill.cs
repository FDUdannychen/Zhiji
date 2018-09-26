using System;
using System.Collections.Generic;
using System.Text;
using NodaTime;
using Zhiji.Common.Domain;

namespace Zhiji.Bills.Domain.Bills
{
    public class Bill : Entity, IAggregateRoot
    {
        public int ContractId { get; private set; }
        public int TemplateId { get; private set; }
        public int CustomerId { get; private set; }
        public int TenementId { get; private set; }
        public Instant StartDate { get; private set; }
        public Instant EndDate { get; private set; }
        public BillStatus Status { get; private set; }

        public int? _statusId;

        private Bill() { }

        public Bill(
            int contractId,
            int templateId,
            int customerId,
            int tenementId,            
            Instant start, 
            Instant end)
        {
            this.ContractId = contractId;
            this.TemplateId = templateId;
            this.CustomerId = customerId;
            this.TenementId = tenementId;
            this.StartDate = start;
            this.EndDate = end;
            _statusId = BillStatus.Created.Id;
        }

        public void SetPaidStatus()
        {
            if (this.Status == BillStatus.Created)
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
