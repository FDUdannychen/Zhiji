using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Organization.Domain.Branches
{
    public partial class Branch : Entity, IAggregateRoot
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
