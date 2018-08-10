using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.Bills.Api.Models.Contracts;
using Zhiji.Bills.Domain.Bills;

namespace Zhiji.Bills.Api.Models.Bills
{
    public class ViewBill
    {
        public string Name { get; set; }

        public ViewContract Contract { get; set; }

        public BillPeriod Period { get; set; }

        public BillStatus Status { get; set; }
    }
}
