using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zhiji.Common.Domain;

namespace Zhiji.Customers.Domain.Customers
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetAsync(int id);

        Customer Add(Customer customer);
    }
}
