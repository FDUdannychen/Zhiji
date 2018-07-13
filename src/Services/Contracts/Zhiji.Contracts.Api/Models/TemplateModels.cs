using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhiji.Contracts.Api.Models
{
    public class CreateTemplate
    {
        public string Name { get; set; }
    }

    public class ViewTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
