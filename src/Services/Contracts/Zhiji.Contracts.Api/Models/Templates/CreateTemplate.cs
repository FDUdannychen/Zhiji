using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhiji.Contracts.Api.Models.Templates
{
    public class CreateTemplate
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int BillingModeId { get; set; }

        public BillingDate BillingDate { get; set; }

        public int BillingPeriodMonth { get; set; }

        public int BillingPeriodStartMonthOffset { get; set; }

        public string TimeZone { get; set; }
    }
}
