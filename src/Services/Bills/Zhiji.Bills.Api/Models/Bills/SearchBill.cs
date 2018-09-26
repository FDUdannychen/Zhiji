using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NodaTime;
using Zhiji.Common.Models;

namespace Zhiji.Bills.Api.Models.Bills
{
    public class SearchBill
    {
        public int? CustomerId { get; set; }

        public int? TenementId { get; set; }

        public int? ContractId { get; set; }

        public int? TemplateId { get; set; }

        public Range<Instant>? StartDateRange { get; set; }

        public Range<Instant>? EndDateRange { get; set; }

        //public Instant? StartDateLowerBound { get; set; }
        //public bool IncludeStartDateLowerBound { get; set; } = true;

        //public Instant? StartDateUpperBound { get; set; }
        //public bool IncludeStartDateUpperBound { get; set; } = false;

        //public Instant? EndDateLowerBound { get; set; }
        //public bool IncludeEndDateLowerBound { get; set; } = true;

        //public Instant? EndDateUpperBound { get; set; }
        //public bool IncludeEndDateUpperBound { get; set; } = false;

        public int? StatusId { get; set; }
    }
}
