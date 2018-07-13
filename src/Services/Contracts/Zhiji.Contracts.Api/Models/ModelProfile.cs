using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.Api.Models
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            this.CreateMap<Template, ViewTemplate>();
        }
    }
}
