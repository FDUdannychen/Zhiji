using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Zhiji.Organizations.Domain.Employees;

namespace Zhiji.Organizations.Api.Models.Employees
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployee>
    {
        public UpdateEmployeeValidator()
        {
            this.RuleFor(m => m.EmployeeId)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            this.RuleFor(m => m.Name)
                .Length(Employee.NameMinLength, Employee.NameMaxLength);

            this.RuleFor(m => m.DepartmentId)
                .GreaterThanOrEqualTo(1);
        }
    }
}
