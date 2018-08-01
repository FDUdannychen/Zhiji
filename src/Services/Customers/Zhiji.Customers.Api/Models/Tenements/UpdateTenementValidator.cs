using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Zhiji.Customers.Api.Models.Tenements
{
    public class UpdateTenementValidator : AbstractValidator<UpdateTenement>
    {
        public UpdateTenementValidator()
        {
            this.RuleFor(m => m.TenementId)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            this.RuleFor(m => m.OwnerId)
                .GreaterThanOrEqualTo(1);
        }
    }
}
