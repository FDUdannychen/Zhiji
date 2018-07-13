using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Zhiji.Customers.Api.Models.Tenements
{
    public class CreateTenementValidator : AbstractValidator<CreateTenement>
    {
        public CreateTenementValidator(IValidator<Address> addressValidator)
        {
            this.RuleFor(m => m.OwnerId)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            this.RuleFor(m => m.Address)
                .NotNull()
                .SetValidator(addressValidator);
        }
    }
}
