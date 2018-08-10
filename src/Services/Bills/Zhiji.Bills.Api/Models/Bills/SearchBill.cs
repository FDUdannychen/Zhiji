using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.Bills.Domain.Bills;

namespace Zhiji.Bills.Api.Models.Bills
{
    public class SearchBill
    {
        public int? CustomerId { get; set; }

        public int? TenementId { get; set; }

        public int? ContractId { get; set; }

        public int? TemplateId { get; set; }

        public int? BillStatusId { get; set; }
    }
}
