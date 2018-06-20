using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Organization.Domain.Companies
{
    public partial class Company : Entity, IAggregateRoot
    {
        private string _name;
        private int? _parentId;
        private Company _parent;

        public Company(string name, int? parentId)
        {
            _name = name;
            _parentId = parentId;
        }

        public string Name => _name;
        public int? ParentId => _parentId;
        public Company Parent => _parent;
    }
}
