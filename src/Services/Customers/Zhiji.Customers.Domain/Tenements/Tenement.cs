﻿using System;
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

        private Tenement() { }

        public Tenement(Address address, int ownerId)
        {
            this.Address = address;
            _ownerId = ownerId;
        }

        public Address Address { get; private set; }
        public int OwnerId => _ownerId;
        public Customer Owner => _owner;
    }
}
