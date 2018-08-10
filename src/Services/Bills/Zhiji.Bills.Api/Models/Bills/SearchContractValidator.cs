using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Zhiji.Common.Domain;
using Zhiji.Bills.Domain.Bills;

namespace Zhiji.Bills.Api.Models.Bills
{
    public class SearchBillValidator : AbstractValidator<SearchBill>
    {
        public SearchBillValidator()
        {
            this.RuleFor(m => m.CustomerId)                
                .NotEmpty().When(m => m.TenementId is null && m.ContractId is null && m.TemplateId is null)
                .GreaterThanOrEqualTo(1);

            this.RuleFor(m => m.TenementId)
                .NotEmpty().When(m => m.CustomerId is null && m.ContractId is null && m.TemplateId is null)
                .GreaterThanOrEqualTo(1);

            this.RuleFor(m => m.TemplateId)
                .NotEmpty().When(m => m.CustomerId is null && m.TenementId is null && m.ContractId is null)
                .GreaterThanOrEqualTo(1);

            this.RuleFor(m => m.ContractId)
                .NotEmpty().When(m => m.CustomerId is null && m.TenementId is null && m.TemplateId is null)
                .GreaterThanOrEqualTo(1);

            this.RuleFor(m => m.BillStatusId)
                .Must(v => v is null || Enumeration.ContainsValue<BillStatus>(v.Value));
        }
    }
}
