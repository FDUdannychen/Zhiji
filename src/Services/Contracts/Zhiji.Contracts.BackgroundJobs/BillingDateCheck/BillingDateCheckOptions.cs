using System;

namespace Zhiji.Contracts.BackgroundJobs.BillingDateCheck
{
    class BillingDateCheckOptions
    {
        public TimeSpan Interval { get; set; } = TimeSpan.FromHours(6);
    }
}
