using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhiji.Customers.Api.Models.Tenements
{
    public class CreateTenement
    {
        public int OwnerId { get; set; }

        public Address Address { get; set; }
    }
}
