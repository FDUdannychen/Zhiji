using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;
using Zhiji.Customers.Domain.Customers;

namespace Zhiji.Customers.Domain.Tenements
{
    public partial class Tenement : Entity, IAggregateRoot
    {
        private int _ownerId;
        private Customer _owner;
        private decimal _area;
        private int _typeId;

        private Tenement() { }

        public Tenement(Address address, int ownerId, int typeId)
        {
            this.Address = address;
            _ownerId = ownerId;
            _typeId = typeId;
        }

        public Address Address { get; private set; }
        public Customer Owner => _owner;
        public decimal Area => _area;
        public TenementType Type { get; private set; }
    }
}
