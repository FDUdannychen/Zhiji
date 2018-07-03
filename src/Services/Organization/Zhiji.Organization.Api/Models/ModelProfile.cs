using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zhiji.Organization.Domain.Companies;
using Zhiji.Organization.Domain.Departments;
using Zhiji.Organization.Domain.Employees;

namespace Zhiji.Organization.Api.Models
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
