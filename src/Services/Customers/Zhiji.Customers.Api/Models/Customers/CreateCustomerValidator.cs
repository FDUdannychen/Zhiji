using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Zhiji.Customers.Domain.Customers;

namespace Zhiji.Customers.Api.Models.Customers
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomer>
    {
        public CreateCustomerValidator(IValidator<Address> addressValidator)
        {
            this.RuleFor(m => m.Name)
                .NotEmpty()
                .Length(Customer.NameMinLength, Customer.NameMaxLength);

            this.RuleFor(m => m.Address)
                .NotNull()
                .SetValidator(addressValidator);
        }
    }
}
