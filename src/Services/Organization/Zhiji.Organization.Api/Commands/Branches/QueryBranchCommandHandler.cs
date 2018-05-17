using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zhiji.Organization.Domain.Branches;
using Zhiji.Organization.Infrastructure;

namespace Zhiji.Organization.Api.Commands.Branches
{
    public class QueryBranchCommandHandler : IRequestHandler<QueryBranchCommand, IEnumerable<Branch>>
    {
        private readonly OrganizationContext _context;

        public QueryBranchCommandHandler(OrganizationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Branch>> Handle(QueryBranchCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Branch> query = _context.Branches;

            if (request.Id.HasValue)
            {
                query = query.Where(b => b.Id == request.Id.Value);
            }

            return await query.AsNoTracking().ToListAsync(cancellationToken);            
        }
    }
}
