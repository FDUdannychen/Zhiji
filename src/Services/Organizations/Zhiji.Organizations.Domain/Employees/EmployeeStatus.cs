using System;
using System.Collections.Generic;
using System.Text;
using Zhiji.Common.Domain;

namespace Zhiji.Organizations.Domain.Employees
{
    public class EmployeeStatus : Enumeration
    {
        public static readonly EmployeeStatus Normal = new EmployeeStatus(1, nameof(Normal));
        public static readonly EmployeeStatus Demission = new EmployeeStatus(2, nameof(Demission));

        public EmployeeStatus(int id, string name)
            : base(id, name)
        { }
    }
}
