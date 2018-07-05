using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.Organizations.Domain.Employees;

namespace Zhiji.Organizations.Api.Models
{
    public class CreateEmployee
    {
        [Required]
        [MinLength(Employee.NameMinLength)]
        [MaxLength(Employee.NameMaxLength)]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int DepartmentId { get; set; }
    }
}
