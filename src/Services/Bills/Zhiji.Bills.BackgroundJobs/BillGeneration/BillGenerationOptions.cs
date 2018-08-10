using System;

namespace Zhiji.Bills.BackgroundJobs.BillGeneration
{
    public class BillGenerationOptions
    {
        public TimeSpan Interval { get; set; } = TimeSpan.FromHours(6);
    }
}
