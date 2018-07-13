using System;
using Zhiji.Common.Domain;

namespace Zhiji.Contracts.Domain.Templates
{
    public partial class Template : Entity, IAggregateRoot
    {
        private string _name;

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
