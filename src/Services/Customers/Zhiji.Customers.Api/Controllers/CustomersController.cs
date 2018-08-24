using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zhiji.Customers.Api.Models.Customers;
using Zhiji.Customers.Domain.Customers;

namespace Zhiji.Customers.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerQuery _customerQuery;
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(IMapper mapper,
            ICustomerQuery customerQuery,
            ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerQuery = customerQuery;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ViewCustomer), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ViewCustomer>> GetAsync(int id, CancellationToken cancellationToken)
        {
            var customer = await _customerQuery.GetAsync(id, cancellationToken);
            if (customer is null) return NotFound();
            return _mapper.Map<ViewCustomer>(customer);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ViewCustomer), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAsync([FromBody]CreateCustomer request, CancellationToken cancellationToken)
        {
            var address = _mapper.Map<Domain.Address>(request.Address);
            var customer = new Customer(request.Name, address);
            _customerRepository.Add(customer);
            await _customerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            var vm = _mapper.Map<ViewCustomer>(customer);
            return CreatedAtAction(nameof(GetAsync), new { id = vm.Id }, vm);
        }

        [HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]        
        public async Task<IActionResult> UpdateAsync([FromBody]UpdateCustomer request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetAsync(request.CustomerId, cancellationToken);
            if (customer is null) return NotFound();

            if (request.Name != null) customer.ChangeName(request.Name);
            if (request.Address != null)
            {
                var address = _mapper.Map<Domain.Address>(request.Address);
                customer.ChangeAddress(address);
            }

            await _customerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}
