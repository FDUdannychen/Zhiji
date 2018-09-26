using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Zhiji.Bills.Domain.Bills;
using Zhiji.Common.Domain;

namespace Zhiji.Bills.Api.Models.Bills
{
    public class SearchBillValidator : AbstractValidator<SearchBill>
    {
        public SearchBillValidator()
        {
            this.RuleFor(m => m.CustomerId).GreaterThanOrEqualTo(1);
            this.RuleFor(m => m.TenementId).GreaterThanOrEqualTo(1);
            this.RuleFor(m => m.ContractId).GreaterThanOrEqualTo(1);
            this.RuleFor(m => m.TemplateId).GreaterThanOrEqualTo(1);

            this.RuleFor(m => m.StatusId)
                .Must(v => Enumeration.ContainsValue<BillStatus>(v.Value))
                .When(m => m.StatusId.HasValue);
        }

        protected override bool PreValidate(ValidationContext<SearchBill> context, ValidationResult result)
        {
            if (context.InstanceToValidate.CustomerId is null
                && context.InstanceToValidate.TenementId is null
                && context.InstanceToValidate.ContractId is null
                && context.InstanceToValidate.TemplateId is null
                && context.InstanceToValidate.StartDateRange is null
                && context.InstanceToValidate.EndDateRange is null)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, "At least one search condition is required"));
                return false;
            }

            return base.PreValidate(context, result);
        }
    }
}
