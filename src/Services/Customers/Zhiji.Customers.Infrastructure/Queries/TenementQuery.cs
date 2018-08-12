using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Customers.Domain.Tenements;

namespace Zhiji.Customers.Infrastructure.Queries
{
    class TenementQuery : ITenementQuery
    {
        private readonly CustomerQueryContext _context;

        public TenementQuery(CustomerQueryContext context)
            => _context = context;

        public Task<Tenement> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.Tenements
                .Include(e => e.Owner)
                .Include(e => e.Type)
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public Task<Tenement[]> ListAsync(int ownerId, CancellationToken cancellationToken = default)
        {
            return _context.Tenements
                .Include(e => e.Owner)
                .Include(e => e.Type)
                .Where(e => e.Owner.Id == ownerId)
                .ToArrayAsync(cancellationToken);
        }
    }
}
