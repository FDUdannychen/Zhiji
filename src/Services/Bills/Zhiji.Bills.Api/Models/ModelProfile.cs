using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zhiji.Bills.Api.Models.Contracts;
using Zhiji.Bills.Api.Models.Templates;
using Zhiji.Bills.Domain.Contracts;
using Zhiji.Bills.Domain.Templates;

namespace Zhiji.Bills.Api.Models
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            this.CreateMap<Template, ViewTemplate>();
            this.CreateMap<Contract, ViewContract>();
        }
    }
}
