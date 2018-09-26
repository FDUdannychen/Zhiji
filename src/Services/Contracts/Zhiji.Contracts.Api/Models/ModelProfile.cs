using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NodaTime;
using Zhiji.Contracts.Api.Models.Contracts;
using Zhiji.Contracts.Api.Models.Templates;
using Zhiji.Contracts.Domain.Contracts;
using Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.Api.Models
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            this.CreateMap<Template, ViewTemplate>();

            this.CreateMap<Contract, ViewContract>()
                .ForMember(v => v.StartDate,
                    o => o.ResolveUsing(c => c.Start.InZone(c.Template.TimeZone).Date))
                .ForMember(v => v.EndDate,
                    o => o.ResolveUsing(c => c.End is null ? (LocalDate?)null : c.End.Value.InZone(c.Template.TimeZone).Date));

            this.CreateMap<Domain.Templates.BillingDate, Templates.BillingDate>()
                .ReverseMap()
                .ConstructUsing(m => new Domain.Templates.BillingDate(m.Month, m.Day, m.IntervalMonth));
        }
    }
}
