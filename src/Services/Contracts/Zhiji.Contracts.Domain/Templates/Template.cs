using System;
using Zhiji.Common.Domain;

namespace Zhiji.Contracts.Domain.Templates
{
    public partial class Template : Entity, IAggregateRoot
    {
        private string _name;
        private decimal _price;

        private Template() { }

        public Template(string name)
            : this()
        {
            _name = name;
        }

        public string Name => _name;
        public decimal Price => _price;
        public BillingMode BillingMode { get; private set; }
        public BillingDate BillingDate { get; private set; }

        public void ChangeName(string name)
        {
            _name = name;
        }
    }
}
