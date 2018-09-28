using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NodaTime;

namespace Zhiji.Contracts.Api.Models.Templates
{
    public class BillingDate
    {
        public int Month { get; set; }

        public int Day { get; set; }

        public int IntervalMonth { get; set; }
    }
}
