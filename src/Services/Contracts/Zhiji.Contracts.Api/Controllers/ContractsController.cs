using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using Zhiji.Contracts.Api.Models.Contracts;
using Zhiji.Contracts.Domain.Contracts;
using Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITemplateQuery _templateQuery;
        private readonly IContractQuery _contractQuery;
        private readonly IContractRepository _contractRepository;
                
        public ContractsController(IMapper mapper,
            ITemplateQuery templateQuery,
            IContractQuery contractQuery,
            IContractRepository contractRepository)
        {
            _mapper = mapper;
            _templateQuery = templateQuery;
            _contractQuery = contractQuery;
            _contractRepository = contractRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ViewContract), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ViewContract>> GetAsync(int id, CancellationToken cancellationToken)
        {
            var contract = await _contractQuery.GetAsync(id, cancellationToken);
            if (contract is null) return NotFound();
            return _mapper.Map<ViewContract>(contract);
        }

        [HttpGet]        
        [ProducesResponseType(typeof(ViewContract[]), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ViewContract[]>> SearchAsync([FromQuery]SearchContract request, CancellationToken cancellationToken)
        {
            var contracts = await _contractQuery.ListAsync(request.CustomerId,
                request.TenementId,
                request.TemplateId,
                request.StartDateRange,
                request.EndDateRange,
                cancellationToken);

            return _mapper.Map<ViewContract[]>(contracts);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAsync([FromBody]CreateContract request, CancellationToken cancellationToken)
        {
            var contract = new Contract(request.TemplateId, request.CustomerId, request.TenementId, request.StartDate, request.EndDate);
            _contractRepository.Add(contract);
            await _contractRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(GetAsync), new { id = contract.Id }, new { ContractId = contract.Id });
        }
    }
}
