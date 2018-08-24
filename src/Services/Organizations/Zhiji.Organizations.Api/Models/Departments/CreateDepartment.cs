using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhiji.Organizations.Api.Models.Departments
{
    public class CreateDepartment
    {
        public string Name { get; set; }
        
        public int? ParentId { get; set; }
        
        public int CompanyId { get; set; }
    }
}
