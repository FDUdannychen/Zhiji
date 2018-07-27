using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Zhiji.Bills.Domain.Templates;

namespace Zhiji.Bills.Api.Models.Templates
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
