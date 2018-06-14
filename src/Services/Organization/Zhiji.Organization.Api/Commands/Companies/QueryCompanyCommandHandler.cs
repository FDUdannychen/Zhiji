using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zhiji.Organization.Domain.Companies;
using Zhiji.Organization.Infrastructure;

namespace Zhiji.Organization.Api.Commands.Companies
{
    public class QueryCompanyCommandHandler : IRequestHandler<QueryCompanyCommand, IEnumerable<Company>>
    {
        private readonly OrganizationContext _context;

        public QueryCompanyCommandHandler(OrganizationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> Handle(QueryCompanyCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Company> query = _context.Companies;

            if (request.Id.HasValue)
            {
                query = query.Where(b => b.Id == request.Id.Value);
            }

            return await query.ToListAsync(cancellationToken);            
        }
    }
}
