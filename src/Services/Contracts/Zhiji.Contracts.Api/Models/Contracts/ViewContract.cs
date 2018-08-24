using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NodaTime;
using Zhiji.Contracts.Api.Models.Templates;

namespace Zhiji.Contracts.Api.Models.Contracts
{
    public class ViewContract
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int TenementId { get; set; }

        public LocalDate StartDate { get; set; }

        public LocalDate? EndDate { get; set; }

        public ViewTemplate Template { get; set; }
    }
}
