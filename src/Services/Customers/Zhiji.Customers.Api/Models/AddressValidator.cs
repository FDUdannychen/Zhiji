using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Zhiji.Customers.Api.Models
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            this.RuleFor(m => m.Country)
                .NotEmpty()
                .Length(Domain.Address.CountryMinLength, Domain.Address.CountryMaxLength);

            this.RuleFor(m => m.Province)
                .NotEmpty()
                .Length(Domain.Address.ProvinceMinLength, Domain.Address.ProvinceMaxLength);

            this.RuleFor(m => m.City)
                .NotEmpty()
                .Length(Domain.Address.CityMinLength, Domain.Address.CityMaxLength);

            this.RuleFor(m => m.Street)
                .NotEmpty()
                .Length(Domain.Address.StreetMinLength, Domain.Address.StreetMaxLength);
        }
    }
}
