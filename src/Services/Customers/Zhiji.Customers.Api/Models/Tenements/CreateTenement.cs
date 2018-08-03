using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhiji.Customers.Api.Models.Tenements
{
    public class CreateTenement
    {
        public Address Address { get; set; }

        public int OwnerId { get; set; }        

        public decimal Area { get; set; }

        public int TypeId { get; set; }
    }
}
