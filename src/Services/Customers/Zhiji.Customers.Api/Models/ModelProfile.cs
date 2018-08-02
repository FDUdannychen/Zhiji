using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zhiji.Customers.Api.Models.Customers;
using Zhiji.Customers.Api.Models.Tenements;
using Zhiji.Customers.Domain.Customers;
using Zhiji.Customers.Domain.Tenements;

namespace Zhiji.Customers.Api.Models
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            this.CreateMap<Customer, ViewCustomer>();

            this.CreateMap<Domain.Address, Address>();
            this.CreateMap<Address, Domain.Address>()
                .ConstructUsing(s => new Domain.Address(s.Country, s.Province, s.City, s.Street));

            this.CreateMap<Tenement, ViewTenement>();
        }
    }
}
