using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Common.Infrastructure;
using Zhiji.Customers.Domain.Tenements;

namespace Zhiji.Customers.Infrastructure.Repositories
{
    class TenementRepository : RepositoryBase<CustomerContext, Tenement>, ITenementRepository
    {
        public TenementRepository(CustomerContext context)
            : base(context)
        { }

        public Task<Tenement[]> ListAsync(int ownerId)
        {
            return _context.Tenements
                .Include(e => e.Owner)
                .Include(e => e.Type)
                .Where(e => e.Owner.Id == ownerId)
                .ToArrayAsync();
        }

        public override Task<Tenement> GetAsync(int id)
        {
            return _context.Tenements
                .Include(e => e.Owner)
                .Include(e => e.Type)
                .SingleOrDefaultAsync(e => e.Id == id);
        }
    }
}
