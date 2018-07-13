using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Zhiji.Organizations.Domain.Departments;

namespace Zhiji.Organizations.Api.Models.Departments
{
    public class CreateDepartmentValidator : AbstractValidator<CreateDepartment>
    {
        public CreateDepartmentValidator()
        {
            this.RuleFor(m => m.Name)
                .NotEmpty()
                .Length(Department.NameMinLength, Department.NameMaxLength);

            this.RuleFor(m => m.ParentId)
                .GreaterThanOrEqualTo(1);

            this.RuleFor(m => m.CompanyId)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
        }
    }
}
