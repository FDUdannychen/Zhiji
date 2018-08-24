using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Zhiji.Common.Domain;
using Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.Api.Models.Templates
{
    public class BillingDateValidator : AbstractValidator<BillingDate>
    {
        public BillingDateValidator()
        {
            this.RuleFor(m => m.Month)
                .NotEmpty()
                .InclusiveBetween(1, 12);

            this.RuleFor(m => m.Day)
                .NotEmpty()
                .InclusiveBetween(1, 28);

            this.RuleFor(m => m.IntervalMonth)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
