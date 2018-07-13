using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
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
        }
    }
}
