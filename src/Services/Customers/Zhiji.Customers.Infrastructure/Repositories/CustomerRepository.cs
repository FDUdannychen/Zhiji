using System;
using Zhiji.Common.Infrastructure;
using Zhiji.Customers.Domain.Customers;

namespace Zhiji.Customers.Infrastructure.Repositories
{
    class CustomerRepository : RepositoryBase<CustomerContext, Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext context)
            : base(context)
        { }
    }
}
