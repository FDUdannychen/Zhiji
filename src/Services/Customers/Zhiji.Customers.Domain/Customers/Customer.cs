using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Zhiji.Common.Domain;

namespace Zhiji.Customers.Domain.Customers
{
    public partial class Customer : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public Address Address { get; private set; }

        private Customer() { }

        public Customer(string name, Address address)
        {
            this.Name = name;
            this.Address = address;
        }

        public void ChangeName(string newName)
        {
            this.Name = newName;
        }

        public void ChangeAddress(Address address)
        {
            this.Address = address;
        }
    }
}
