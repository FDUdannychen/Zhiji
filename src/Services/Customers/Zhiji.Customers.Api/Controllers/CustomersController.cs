using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zhiji.Common.AspNetCore;
using Zhiji.Customers.Domain;

namespace Zhiji.Customers.Api.Controllers
{
    public class CustomersController : ApiControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<Customer> Get(int id)
        {
            return await _customerRepository.GetAsync(id);
        }
    }
}
