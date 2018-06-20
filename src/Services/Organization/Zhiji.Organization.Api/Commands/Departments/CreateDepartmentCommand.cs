using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Zhiji.Organization.Domain.Departments;

namespace Zhiji.Organization.Api.Commands.Departments
{
    public class CreateDepartmentCommand : IRequest<Department>
    {
        [Required]
        [MaxLength(Department.NameMaxLength)]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int? ParentId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CompanyId { get; set; }
    }
}
