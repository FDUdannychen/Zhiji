using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Zhiji.Contracts.Api.Models.Contracts
{
    public class CreateContractValidator : AbstractValidator<CreateContract>
    {
        public CreateContractValidator()
        {
            this.RuleFor(m => m.CustomerId)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            this.RuleFor(m => m.TenementId)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            this.RuleFor(m => m.TemplateId)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
        }
    }
}
