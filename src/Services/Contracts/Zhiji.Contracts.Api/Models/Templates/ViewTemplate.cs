using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.Api.Models.Templates
{
    public class ViewTemplate
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public BillingMode BillingMode { get; set; }

        public BillingDate BillingDate { get; set; }
    }
}
