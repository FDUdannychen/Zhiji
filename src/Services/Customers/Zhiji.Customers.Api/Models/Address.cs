using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhiji.Customers.Api.Models
{
    public class Address
    {
        public string Country { get; set; }
        
        public string Province { get; set; }

        public string City { get; set; }

        public string Street { get; set; }
    }
}
