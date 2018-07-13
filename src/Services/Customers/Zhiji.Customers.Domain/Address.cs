using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;

namespace Zhiji.Customers.Domain
{
    public partial class Address : ValueObject
    {
        public string Country { get; private set; }
        public string Province { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }

        private Address() { }

        public Address(string country, string province, string city, string street)
        {
            this.Country = country;
            this.Province = province;
            this.City = city;
            this.Street = street;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Country;
            yield return this.Province;
            yield return this.City;
            yield return this.Street;
        }
    }
}
