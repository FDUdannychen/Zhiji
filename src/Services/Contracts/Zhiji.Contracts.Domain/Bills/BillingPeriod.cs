﻿using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;

namespace Zhiji.Contracts.Domain.Bills
{
    public class BillingPeriod : ValueObject
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        private BillingPeriod() { }

        public BillingPeriod(DateTime start, DateTime end)
        {
            this.Start = start;
            this.End = end;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Start;
            yield return this.End;
        }
    }
}