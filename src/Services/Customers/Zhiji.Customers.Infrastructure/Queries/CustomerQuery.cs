using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zhiji.Customers.Domain.Customers;

namespace Zhiji.Customers.Infrastructure.Queries
{
    class CustomerQuery : ICustomerQuery
    {
        private readonly CustomerQueryContext _context;

        public CustomerQuery(CustomerQueryContext context)
            => _context = context;

        public Task<Customer> GetAsync(int id, CancellationToken cancellationToken = default)
            => _context.Customers.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }
}
