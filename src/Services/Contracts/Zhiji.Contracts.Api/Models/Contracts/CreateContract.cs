using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NodaTime;

namespace Zhiji.Contracts.Api.Models.Contracts
{
    public class CreateContract
    {
        public int CustomerId { get; set; }

        public int TenementId { get; set; }

        public int TemplateId { get; set; }

        public Instant StartDate { get; set; }

        public Instant? EndDate { get; set; }
    }
}
