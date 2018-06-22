using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zhiji.Organization.Domain.Companies;
using Zhiji.Organization.Domain.Departments;

namespace Zhiji.Organization.Api.ViewModels
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            this.CreateMap<Company, CompanyViewModel>();
            this.CreateMap<Department, DepartmentViewModel>();
        }
    }
}
