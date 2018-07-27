using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.Customers.Api.Models.Customers;

namespace Zhiji.Customers.Api.Models.Tenements
{
    public class ViewTenement
    {
        public int Id { get; set; }

        public ViewCustomer Owner { get; set; }

        public Address Address { get; set; }

        public decimal Area { get; set; }

        public string Type { get; set; }
    }
}
