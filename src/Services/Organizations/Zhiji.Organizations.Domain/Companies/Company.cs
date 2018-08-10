using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Organizations.Domain.Companies
{
    public partial class Company : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public Company Parent { get; private set; }

        private int? _parentId;

        private Company() { }

        public Company(string name, int? parentId)
        {
            this.Name = name;
            _parentId = parentId;
        }
    }
}
