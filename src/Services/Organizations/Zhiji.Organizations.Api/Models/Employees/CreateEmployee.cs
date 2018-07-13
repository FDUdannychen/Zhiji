using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhiji.Organizations.Api.Models.Employees
{
    public class CreateEmployee
    {
        public string Name { get; set; }

        public int DepartmentId { get; set; }
    }
}
