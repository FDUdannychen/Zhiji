using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NodaTime;
using Zhiji.Common.Domain;
using Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.Api.Models.Templates
{
    public class CreateTemplateValidator : AbstractValidator<CreateTemplate>
    {
        public CreateTemplateValidator(
            IValidator<BillingDate> billingDateValidator,
            IDateTimeZoneProvider dateTimeZoneProvider)
        {
            this.RuleFor(m => m.Name)
                .NotEmpty()
                .Length(Template.NameMinLength, Template.NameMaxLength);

            this.RuleFor(m => m.BillingModeId)
                .Must(Enumeration.ContainsValue<BillingMode>);
                        
            this.RuleFor(m => m.BillingDate)
                .NotNull()
                .SetValidator(billingDateValidator);

            this.RuleFor(m => m.BillingPeriodMonth)
                .NotEmpty()
                .GreaterThan(0);

            this.RuleFor(m => m.BillingPeriodStartMonthOffset)
                .NotEmpty()
                .GreaterThan(0).When(m => m.BillingModeId == BillingMode.Prepaid.Id)
                .LessThan(0).When(m => m.BillingModeId == BillingMode.Postpaid.Id);

            this.RuleFor(m => m.TimeZone)
                .NotEmpty()
                .Must(dateTimeZoneProvider.Ids.Contains);
        }
    }
}
