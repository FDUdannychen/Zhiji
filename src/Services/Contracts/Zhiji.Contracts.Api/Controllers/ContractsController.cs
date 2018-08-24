﻿using System;
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
            var contracts = await _contractQuery.ListAsync(request.CustomerId, request.TenementId, request.TemplateId, cancellationToken);
            return _mapper.Map<ViewContract[]>(contracts);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ViewContract), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAsync([FromBody]CreateContract request, CancellationToken cancellationToken)
        {
            var template = await _templateQuery.GetAsync(request.TemplateId);
            if (template is null)
            {
                this.ModelState.AddModelError(nameof(CreateContract.TemplateId), $"The template id {request.TemplateId} doesn't exist");
                return BadRequest(this.ModelState);
            }

            var startDate = request.StartDate.AtStartOfDayInZone(template.TimeZone).ToInstant();
            var endDate = request.EndDate.HasValue 
                ? request.EndDate.Value.AtStartOfDayInZone(template.TimeZone).ToInstant()
                : (Instant?)null;
            var contract = new Contract(request.TemplateId, request.CustomerId, request.TenementId, startDate, endDate);
            _contractRepository.Add(contract);
            await _contractRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            var vm = _mapper.Map<ViewContract>(contract);
            return CreatedAtAction(nameof(GetAsync), new { id = vm.Id }, vm);
        }
    }
}
