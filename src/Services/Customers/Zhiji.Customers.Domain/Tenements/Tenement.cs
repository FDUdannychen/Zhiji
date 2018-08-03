using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;
using Zhiji.Customers.Domain.Customers;

namespace Zhiji.Customers.Domain.Tenements
{
    public partial class Tenement : Entity, IAggregateRoot
    {
        public Address Address { get; private set; }
        public Customer Owner { get; private set; }
        public decimal Area { get; private set; }        
        public TenementType Type { get; private set; }

        private int? _ownerId;        
        private int? _typeId;

        private Tenement() { }

        public Tenement(Address address, int ownerId, decimal area, int typeId)
        {
            this.Address = address;
            _ownerId = ownerId;
            this.Area = area;
            _typeId = typeId;
        }

        
    }
}
