using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhiji.Organizations.Api.Models
{
    public class ViewDepartment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ViewDepartment Parent { get; set; }

        public ViewCompany Company { get; set; }
    }
}
