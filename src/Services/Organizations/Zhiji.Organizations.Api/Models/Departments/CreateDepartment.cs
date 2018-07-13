using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.Common.Api;
using Zhiji.Organizations.Api.Models.Companies;
using Zhiji.Organizations.Domain.Departments;

namespace Zhiji.Organizations.Api.Models.Departments
{
    public class CreateDepartment
    {
        public string Name { get; set; }
        
        public int? ParentId { get; set; }
        
        public int CompanyId { get; set; }
    }
}
