using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;
using Zhiji.Organizations.Domain.Companies;

namespace Zhiji.Organizations.Domain.Departments
{
    public partial class Department : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public Department Parent { get; private set; }
        public Company Company { get; private set; }

        private int? _parentId;
        private int? _companyId;

        private Department() { }

        public Department(string name, int? parentId, int companyId)
        {
            this.Name = name;
            _parentId = parentId;
            _companyId = companyId;
        }
    }
}
