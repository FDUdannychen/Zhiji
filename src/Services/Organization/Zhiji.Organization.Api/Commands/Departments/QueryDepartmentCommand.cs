using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Zhiji.Organization.Domain.Departments;

namespace Zhiji.Organization.Api.Commands.Departments
{
    public class QueryDepartmentCommand : IRequest<IEnumerable<Department>>
    {
        [Range(1, int.MaxValue)]
        public int? Id { get; set; }

        [Range(1, int.MaxValue)]
        public int? CompanyId { get; set; }
    }
}
