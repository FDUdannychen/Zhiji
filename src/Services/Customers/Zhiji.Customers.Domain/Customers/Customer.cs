using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Zhiji.Common.Domain;

namespace Zhiji.Customers.Domain.Customers
{
    public partial class Customer : Entity, IAggregateRoot
    {
        private string _name;

        private Customer() { }

        public Customer(string name, Address address) : this()
        {
            _name = name;
            this.Address = address;
        }

        public string Name => _name;
        public Address Address { get; private set; }

        public void ChangeName(string newName)
        {
            _name = newName;
        }

        public void ChangeAddress(Address address)
        {
            this.Address = address;
        }
    }
}
