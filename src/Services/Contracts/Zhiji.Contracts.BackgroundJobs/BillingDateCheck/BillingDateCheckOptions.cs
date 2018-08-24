using System;

namespace Zhiji.Contracts.BackgroundJobs.BillingDateCheck
{
    public class BillingDateCheckOptions
    {
        public TimeSpan Interval { get; set; } = TimeSpan.FromHours(6);
    }
}
