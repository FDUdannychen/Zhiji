using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.Common.Api;
using Zhiji.Organizations.Domain.Companies;

namespace Zhiji.Organizations.Api.Models.Companies
{
    public class ViewCompany
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ViewCompany Parent { get; set; }
    }
}
