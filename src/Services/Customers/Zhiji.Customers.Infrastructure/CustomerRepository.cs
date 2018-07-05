using System;
using Zhiji.Common.Domain;
using Zhiji.Common.Infrastructure;
using Zhiji.Customers.Domain;

namespace Zhiji.Customers.Infrastructure
{
    class CustomerRepository : RepositoryBase<CustomerContext, Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext context)
            : base(context)
        { }
    }
}
