using System;
using NodaTime;
using Zhiji.Common.Domain;

namespace Zhiji.Contracts.Domain.Templates
{
    public partial class Template : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public BillingMode BillingMode { get; private set; }
        public BillingDate BillingDate { get; private set; }        
        public int BillingPeriodMonth { get; private set; }
        public int BillingPeriodStartMonthOffset { get; private set; }
        public DateTimeZone TimeZone { get; private set; }

        private int? _billingModeId;

        private Template() { }

        public Template(string name, decimal price, int billingModeId, BillingDate billingDate, int billingPeriodMonth, int billingPeriodStartMonthOffset, DateTimeZone timeZone)
        {
            _billingModeId = billingModeId;

            this.Name = name;
            this.Price = price;
            this.BillingDate = billingDate;
            this.BillingPeriodMonth = billingPeriodMonth;
            this.BillingPeriodStartMonthOffset = billingPeriodStartMonthOffset;
            this.TimeZone = timeZone;
        }

        public void ChangeName(string name)
        {
            this.Name = name;
        }
    }
}
