using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Zhiji.Common.Api;
using Zhiji.Organizations.Domain.Companies;

namespace Zhiji.Organizations.Api.Models.Companies
{
    public class CreateCompanyValidator : AbstractValidator<CreateCompany>
    {
        public CreateCompanyValidator()
        {
            this.RuleFor(m => m.Name)
                .NotEmpty()
                .Length(Company.NameMinLength, Company.NameMaxLength);

            this.RuleFor(m => m.ParentId)
                .GreaterThanOrEqualTo(1);
        }
    }
}
