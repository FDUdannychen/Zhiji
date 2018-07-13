using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Zhiji.Customers.Domain.Customers;

namespace Zhiji.Customers.Api.Models.Customers
{
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomer>
    {
        public UpdateCustomerValidator(IValidator<Address> addressValidator)
        {
            this.RuleFor(m => m.CustomerId)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            this.RuleFor(m => m.Name)
                .Length(Customer.NameMinLength, Customer.NameMaxLength);

            this.RuleFor(m => m.Address)
                .SetValidator(addressValidator);
        }
    }
}
