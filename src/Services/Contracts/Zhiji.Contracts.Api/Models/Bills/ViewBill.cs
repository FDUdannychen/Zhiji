using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.Contracts.Api.Models.Contracts;
using Zhiji.Contracts.Domain.Bills;

namespace Zhiji.Contracts.Api.Models.Bills
{
    public class ViewBill
    {
        public string Name { get; set; }

        public ViewContract Contract { get; set; }

        public BillPeriod Period { get; set; }

        public BillStatus Status { get; set; }
    }
}
