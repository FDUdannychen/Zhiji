using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;

namespace Zhiji.Customers.Domain
{
    public partial class Tenement : Entity
    {
        public Address Address { get; private set; }

        private Tenement() { }

        public Tenement(Address address)
        {
            this.Address = address;
        }
    }
}
