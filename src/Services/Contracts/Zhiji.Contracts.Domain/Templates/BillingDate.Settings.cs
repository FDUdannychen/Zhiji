using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;

namespace Zhiji.Contracts.Domain.Templates
{
    public partial class BillingDate
    {
        public static readonly int MinMonthValue = 1;
        public static readonly int MaxMonthValue = 12;
        public static readonly int MaxMonthValueWhenBillingModeQuarter = 3;

        public static readonly int MinDayValue = 1;
        public static readonly int MaxDayValue = 28;
    }
}
