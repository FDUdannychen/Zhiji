using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zhiji.Common.Api;
using Zhiji.Bills.Api.Models.Contracts;
using Zhiji.Bills.Domain.Contracts;

namespace Zhiji.Bills.Api.Controllers
{
    public class ContractsController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IContractRepository _contractRepository;

        public ContractsController(IMapper mapper,
            IContractRepository contractRepository)
        {
            _mapper = mapper;
            _contractRepository = contractRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ViewContract), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ViewContract>> Get(int id)
        {
            var contract = await _contractRepository.GetAsync(id);
            if (contract is null) return NotFound();
            return _mapper.Map<ViewContract>(contract);
        }

        [HttpGet]        
        [ProducesResponseType(typeof(ViewContract[]), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ViewContract[]>> Search([FromQuery]SearchContract request)
        {
            var contracts = await _contractRepository.ListAsync(request.CustomerId, request.TenementId, request.TemplateId);
            return _mapper.Map<ViewContract[]>(contracts);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ViewContract), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody]CreateContract request)
        {
            var contract = new Contract(request.TemplateId, request.CustomerId, request.TenementId);
            _contractRepository.Add(contract);
            await _contractRepository.UnitOfWork.SaveChangesAsync();
            var vm = _mapper.Map<ViewContract>(contract);
            return CreatedAtAction(nameof(Get), new { id = vm.Id }, vm);
        }
    }
}
