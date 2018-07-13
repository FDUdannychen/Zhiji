using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zhiji.Common.Api;
using Zhiji.Customers.Api.Models.Customers;
using Zhiji.Customers.Domain.Customers;

namespace Zhiji.Customers.Api.Controllers
{
    public class CustomersController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(IMapper mapper, 
            ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ViewCustomer), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ViewCustomer>> Get(int id)
        {
            var customer = await _customerRepository.GetAsync(id);
            if (customer is null) return NotFound();
            return _mapper.Map<ViewCustomer>(customer);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ViewCustomer), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody]CreateCustomer request)
        {
            var address = _mapper.Map<Domain.Address>(request.Address);
            var customer = new Customer(request.Name, address);
            _customerRepository.Add(customer);
            await _customerRepository.UnitOfWork.SaveChangesAsync();
            var vm = _mapper.Map<ViewCustomer>(customer);
            return CreatedAtAction(nameof(Get), new { id = vm.Id }, vm);
        }

        [HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]        
        public async Task<IActionResult> Update([FromBody]UpdateCustomer request)
        {
            var customer = await _customerRepository.GetAsync(request.CustomerId);
            if (customer is null) return NotFound();

            if (request.Name != null) customer.ChangeName(request.Name);
            if (request.Address != null)
            {
                var address = _mapper.Map<Domain.Address>(request.Address);
                customer.ChangeAddress(address);
            }

            await _customerRepository.UnitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
