﻿using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;

namespace Zhiji.Bills.Domain.Bills
{
    public class BillPeriod : ValueObject
    {
        public DateTimeOffset Start { get; private set; }
        public DateTimeOffset End { get; private set; }

        private BillPeriod() { }

        public BillPeriod(DateTimeOffset start, DateTimeOffset end)
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
