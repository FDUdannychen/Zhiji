using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Organization.Domain.Companies
{
    public partial class Company : Entity, IAggregateRoot
    {
        public string Name { get; }

        public int? ParentId { get; }

        public Company Parent { get; set; }

        public Company(string name, int? parentId)
        {
            this.Name = name;
            this.ParentId = parentId;
        }
    }
}
