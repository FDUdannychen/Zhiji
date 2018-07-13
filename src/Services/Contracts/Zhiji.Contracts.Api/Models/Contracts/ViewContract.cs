using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhiji.Contracts.Api.Models.Contracts
{
    public class ViewContract
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int TenementId { get; set; }

        public ViewTemplate Template { get; set; }
    }
}
