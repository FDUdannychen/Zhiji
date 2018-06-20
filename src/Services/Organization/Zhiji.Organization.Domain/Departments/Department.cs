using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;
using Zhiji.Organization.Domain.Companies;

namespace Zhiji.Organization.Domain.Departments
{
    public partial class Department : Entity, IAggregateRoot
    {
        private string _name;
        private int? _parentId;
        private Department _parent;
        private int _companyId;
        private Company _company;

        public Department(string name, int? parentId, int companyId)
        {
            _name = name;
            _parentId = parentId;
            _companyId = companyId;
        }

        public string Name => _name;
        public int? ParentId => _parentId;
        public Department Parent => _parent;
        public int CompanyId => _companyId;
        public Company Company => _company;
    }
}
