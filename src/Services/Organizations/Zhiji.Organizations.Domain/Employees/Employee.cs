using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;
using Zhiji.Organizations.Domain.Departments;

namespace Zhiji.Organizations.Domain.Employees
{
    public partial class Employee : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public Department Department { get; private set; }
        public EmployeeStatus Status { get; private set; }

        private int? _departmentId;        
        private int? _statusId;

        private Employee() { }

        public Employee(string name, int departmentId)
        {
            this.Name = name;
            _departmentId = departmentId;
            _statusId = EmployeeStatus.Normal.Id;
        }

        public void ChangeName(string newName)
        {
            this.Name = newName;
        }

        public void TransferDepartment(int targetDepartmentId)
        {
            _departmentId = targetDepartmentId;
        }
    }
}
