using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Zhiji.Bills.Api.Models.Contracts
{
    public class SearchContractValidator : AbstractValidator<SearchContract>
    {
        public SearchContractValidator()
        {
            this.RuleFor(m => m.CustomerId)                
                .NotEmpty().When(m => m.TenementId is null && m.TemplateId is null)
                .GreaterThanOrEqualTo(1);

            this.RuleFor(m => m.TenementId)
                .NotEmpty().When(m => m.CustomerId is null && m.TemplateId is null)
                .GreaterThanOrEqualTo(1);

            this.RuleFor(m => m.TemplateId)
                .NotEmpty().When(m => m.CustomerId is null && m.TenementId is null)
                .GreaterThanOrEqualTo(1);
        }
    }
}
