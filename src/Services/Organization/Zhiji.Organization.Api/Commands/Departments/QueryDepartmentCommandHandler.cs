using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zhiji.Organization.Domain.Departments;
using Zhiji.Organization.Infrastructure;

namespace Zhiji.Organization.Api.Commands.Departments
{
    public class QueryDepartmentCommandHandler : IRequestHandler<QueryDepartmentCommand, IEnumerable<Department>>
    {
        private readonly OrganizationContext _context;

        public QueryDepartmentCommandHandler(OrganizationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> Handle(QueryDepartmentCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Department> query = _context.Departments;

            if (request.Id.HasValue)
            {
                query = query.Where(e => e.Id == request.Id.Value);
            }

            if (request.CompanyId.HasValue)
            {
                query = query.Where(e => e.CompanyId == request.CompanyId.Value);
            }

            return await query.ToListAsync(cancellationToken);            
        }
    }
}
