using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.Organization.Domain.Companies;

namespace Zhiji.Organization.Api.ViewModels
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            this.CreateMap<Company, CompanyViewModel>();
        }
    }
}
