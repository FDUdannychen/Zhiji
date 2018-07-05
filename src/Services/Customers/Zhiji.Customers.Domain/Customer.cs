using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Zhiji.Common.Domain;

namespace Zhiji.Customers.Domain
{
    public partial class Customer : Entity, IAggregateRoot
    {
        private string _name;
        private List<Tenement> _tenements;

        private Customer()
        {
            _tenements = new List<Tenement>();
        }

        public Customer(string name, Address address) : this()
        {
            _name = name;
            this.Address = address;
        }

        public string Name => _name;
        public Address Address { get; private set; }
        public ReadOnlyCollection<Tenement> Tenements => _tenements.AsReadOnly();

        public void AddTenement(Tenement tenement)
        {
            _tenements.Add(tenement);
        }
    }
}
