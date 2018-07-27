using System;
using Zhiji.Common.Domain;

namespace Zhiji.Bills.Domain.Templates
{
    public partial class Template : Entity, IAggregateRoot
    {
        private string _name;
        private decimal _price;

        private Template() { }

        public Template(string name)
        {
            _name = name;
        }

        public string Name => _name;

        public void ChangeName(string name)
        {
            _name = name;
        }
    }
}
