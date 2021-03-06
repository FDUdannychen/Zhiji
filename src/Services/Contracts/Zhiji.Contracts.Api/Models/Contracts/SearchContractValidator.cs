﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace Zhiji.Contracts.Api.Models.Contracts
{
    public class SearchContractValidator : AbstractValidator<SearchContract>
    {
        public SearchContractValidator()
        {
            this.RuleFor(m => m.CustomerId).GreaterThanOrEqualTo(1);
            this.RuleFor(m => m.TenementId).GreaterThanOrEqualTo(1);
            this.RuleFor(m => m.TemplateId).GreaterThanOrEqualTo(1);
        }

        protected override bool PreValidate(ValidationContext<SearchContract> context, ValidationResult result)
        {
            if (context.InstanceToValidate.CustomerId is null
                && context.InstanceToValidate.TenementId is null
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
