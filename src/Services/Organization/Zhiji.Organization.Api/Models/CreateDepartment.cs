using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.Organization.Domain.Departments;

namespace Zhiji.Organization.Api.Models
{
    public class CreateDepartment
    {
        [Required]
        [MinLength(Department.NameMinLength)]
        [MaxLength(Department.NameMaxLength)]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int? ParentId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CompanyId { get; set; }
    }
}
