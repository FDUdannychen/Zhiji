using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Zhiji.Contracts.Domain.Templates;
using DomainModel = Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.Api.Models.Templates
{
    public class BillingDateValidator : AbstractValidator<BillingDate>
    {
        public BillingDateValidator(int billingModeId)
        {
            var month = this.RuleFor(m => m.Month);

            if (billingModeId == BillingMode.Year.Id || billingModeId == BillingMode.Quarter.Id)
            {
                month.NotEmpty();
            }

            var maxValue = billingModeId == BillingMode.Quarter.Id
                ? DomainModel.BillingDate.MaxMonthValueWhenBillingModeQuarter
                : DomainModel.BillingDate.MaxMonthValue;

            month.InclusiveBetween(DomainModel.BillingDate.MinMonthValue, maxValue);
            
            this.RuleFor(m => m.Day)
                .NotEmpty()
                .InclusiveBetween(DomainModel.BillingDate.MinDayValue, DomainModel.BillingDate.MaxDayValue);
        }
    }
}
