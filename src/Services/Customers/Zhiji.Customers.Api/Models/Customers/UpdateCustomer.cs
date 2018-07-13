using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhiji.Customers.Api.Models.Customers
{
    public class UpdateCustomer
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }
    }
}
