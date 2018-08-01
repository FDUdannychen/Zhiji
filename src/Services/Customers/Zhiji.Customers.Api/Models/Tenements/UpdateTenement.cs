using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhiji.Customers.Api.Models.Tenements
{
    public class UpdateTenement
    {
        public int TenementId { get; set; }

        public int? OwnerId { get; set; }
    }
}
