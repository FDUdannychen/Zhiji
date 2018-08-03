using System;

namespace Zhiji.Contracts.BackgroundJobs.BillGeneration
{
    public class BillGenerationOptions
    {
        public TimeSpan Interval { get; set; } = TimeSpan.FromHours(6);
    }
}
