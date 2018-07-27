using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;

namespace Zhiji.Contracts.Domain.Templates
{
    public class BillingDate : ValueObject
    {
        public int? Month { get; private set; }
        public int Day { get; private set; }

        private BillingDate() { }

        public BillingDate(int? month, int day)
        {
            this.Month = month;
            this.Day = day;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Month;
            yield return this.Day;
        }
    }
}
