using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Zhiji.Organization.Domain.Companies;

namespace Zhiji.Organization.Api.Commands.Companies
{
    public class QueryCompanyCommand : IRequest<IEnumerable<Company>>
    {
        public int? Id { get; set; }
    }
}
