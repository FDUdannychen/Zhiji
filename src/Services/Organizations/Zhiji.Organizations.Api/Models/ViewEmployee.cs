using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhiji.Organizations.Api.Models
{
    public class ViewEmployee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ViewDepartment Department { get; set; }
    }
}
