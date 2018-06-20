using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhiji.Organization.Api.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DepartmentViewModel Parent { get; set; }

        public CompanyViewModel Company { get; set; }
    }
}
