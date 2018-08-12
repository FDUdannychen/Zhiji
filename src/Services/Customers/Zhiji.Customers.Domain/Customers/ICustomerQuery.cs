using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Customers.Domain.Customers
{
    public interface ICustomerQuery : IQuery
    {
        Task<Customer> GetAsync(int id, CancellationToken cancellationToken = default);
    }
}
