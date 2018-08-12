using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zhiji.Common.Api;
using Zhiji.Contracts.Api.Models.Templates;
using Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.Api.Controllers
{
    public class TemplatesController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITemplateQuery _templateQuery;
        private readonly ITemplateRepository _templateRepository;

        public TemplatesController(IMapper mapper,
            ITemplateQuery templateQuery,
            ITemplateRepository templateRepository)
        {
            _mapper = mapper;
            _templateQuery = templateQuery;
            _templateRepository = templateRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ViewTemplate), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ViewTemplate>> Get(int id)
        {
            var template = await _templateQuery.GetAsync(id);
            if (template is null) return NotFound();
            return _mapper.Map<ViewTemplate>(template);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ViewTemplate[]), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ViewTemplate[]>> GetAll()
        {
            var templates = await _templateQuery.ListAsync();
            return _mapper.Map<ViewTemplate[]>(templates);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ViewTemplate), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody]CreateTemplate request)
        {
            var billingDate = _mapper.Map<Domain.Templates.BillingDate>(request.BillingDate);
            var template = new Template(request.Name, request.Price, request.BillingModeId, billingDate);
            _templateRepository.Add(template);
            await _templateRepository.UnitOfWork.SaveChangesAsync();
            var vm = _mapper.Map<ViewTemplate>(template);
            return CreatedAtAction(nameof(Get), new { id = vm.Id }, vm);
        }
    }
}
