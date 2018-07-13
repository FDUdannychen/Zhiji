using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Zhiji.Organizations.Domain.Employees;

namespace Zhiji.Organizations.Api.Models.Employees
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployee>
    {
        public CreateEmployeeValidator()
        {
            this.RuleFor(m => m.Name)
                .NotEmpty()
                .Length(Employee.NameMinLength, Employee.NameMaxLength);

            this.RuleFor(m => m.DepartmentId)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
        }
    }
}
