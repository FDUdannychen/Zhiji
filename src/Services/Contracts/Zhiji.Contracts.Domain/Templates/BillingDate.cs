using System;
using System.Collections.Generic;
using System.Text;
using NodaTime;
using Zhiji.Common.Domain;

namespace Zhiji.Contracts.Domain.Templates
{
    public partial class BillingDate : ValueObject
    {
        public int Month { get; private set; }

        public int Day { get; private set; }

        public int IntervalMonth { get; private set; }

        private BillingDate() { }

        public BillingDate(int month, int day, int interval)
        {
            this.Month = month;
            this.Day = day;
            this.IntervalMonth = interval;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Month;
            yield return this.Day;
            yield return this.IntervalMonth;
        }
    }
}
