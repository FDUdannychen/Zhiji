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

        public DateInterval StartDateRange { get; set; }

        public DateInterval EndDateRange { get; set; }

        public int? StatusId { get; set; }
    }
}
