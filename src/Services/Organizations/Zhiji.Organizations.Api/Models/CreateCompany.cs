using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.Organizations.Domain.Companies;

namespace Zhiji.Organizations.Api.Models
{
    public class CreateCompany
    {
        [Required]
        [MinLength(Company.NameMinLength)]
        [MaxLength(Company.NameMaxLength)]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int? ParentId { get; set; }
    }
}
