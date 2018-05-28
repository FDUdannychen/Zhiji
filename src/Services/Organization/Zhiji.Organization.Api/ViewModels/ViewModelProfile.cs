using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.Organization.Domain.Branches;

namespace Zhiji.Organization.Api.ViewModels
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            this.CreateMap<Branch, BranchViewModel>();
        }
    }
}
