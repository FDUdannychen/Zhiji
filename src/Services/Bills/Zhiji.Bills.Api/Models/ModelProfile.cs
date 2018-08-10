using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zhiji.Bills.Api.Models.Bills;
using Zhiji.Bills.Domain.Bills;

namespace Zhiji.Bills.Api.Models
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            this.CreateMap<Bill, ViewBill>();            
        }
    }
}
