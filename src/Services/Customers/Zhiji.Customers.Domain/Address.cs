using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;

namespace Zhiji.Customers.Domain
{
    public class Address : ValueObject
    {
        public String Country { get; private set; }
        public String Province { get; private set; }
        public String City { get; private set; }
        public String Street { get; private set; }

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
