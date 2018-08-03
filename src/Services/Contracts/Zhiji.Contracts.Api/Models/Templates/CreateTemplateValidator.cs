using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Zhiji.Common.Domain;
using Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.Api.Models.Templates
{
    public class CreateTemplateValidator : AbstractValidator<CreateTemplate>
    {
        public CreateTemplateValidator()
        {
            this.RuleFor(m => m.Name)
                .NotEmpty()
                .Length(Template.NameMinLength, Template.NameMaxLength);

            this.RuleFor(m => m.BillingModeId)
                .Must(Enumeration.ContainsValue<BillingMode>);

            this.RuleFor(m => m.BillingDate)
                .NotNull()
                .SetValidator(m => new BillingDateValidator(m.BillingModeId));
        }
    }
}
