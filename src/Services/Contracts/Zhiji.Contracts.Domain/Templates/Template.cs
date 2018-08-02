using System;
using Zhiji.Common.Domain;

namespace Zhiji.Contracts.Domain.Templates
{
    public partial class Template : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public BillingMode BillingMode { get; private set; }
        public BillingDate BillingDate { get; private set; }

        private int? _billingModeId;

        private Template() { }

        public Template(string name, decimal price, int billingModeId)
            : this()
        {
            this.Name = name;
            this.Price = price;
            _billingModeId = billingModeId;
        }

        public void ChangeName(string name)
        {
            this.Name = name;
        }
    }
}
