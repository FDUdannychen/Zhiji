using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.Organizations.Api.Models.Departments;

namespace Zhiji.Organizations.Api.Models.Employees
{
    public class ViewEmployee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ViewDepartment Department { get; set; }
    }
}
