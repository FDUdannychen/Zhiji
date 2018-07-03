using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;
using Zhiji.Organization.Domain.Companies;
using Zhiji.Organization.Domain.Departments;

namespace Zhiji.Organization.Domain.Employees
{
    public partial class Employee : Entity, IAggregateRoot
    {
        private string _name;
        private int _departmentId;
        private Department _department;

        public Employee(string name, int departmentId)
        {
            _name = name;
            _departmentId = departmentId;
        }

        public string Name => _name;
        public int DepartmentId => _departmentId;
        public Department Department => _department;

        public void ChangeName(string newName)
        {
            _name = newName;
        }

        public void TransferDepartment(int targetDepartmentId)
        {
            _departmentId = targetDepartmentId;
        }
    }
}
