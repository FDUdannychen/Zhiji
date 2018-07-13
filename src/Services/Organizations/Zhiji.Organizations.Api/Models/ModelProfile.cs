using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zhiji.Organizations.Api.Models.Companies;
using Zhiji.Organizations.Api.Models.Departments;
using Zhiji.Organizations.Api.Models.Employees;
using Zhiji.Organizations.Domain.Companies;
using Zhiji.Organizations.Domain.Departments;
using Zhiji.Organizations.Domain.Employees;

namespace Zhiji.Organizations.Api.Models
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            this.CreateMap<Company, ViewCompany>();
            this.CreateMap<Department, ViewDepartment>();
            this.CreateMap<Employee, ViewEmployee>();
        }
    }
}
