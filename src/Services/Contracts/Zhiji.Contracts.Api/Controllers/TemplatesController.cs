using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using Zhiji.Contracts.Api.Models.Templates;
using Zhiji.Contracts.Domain.Templates;

namespace Zhiji.Contracts.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITemplateQuery _templateQuery;
        private readonly ITemplateRepository _templateRepository;
        private readonly IDateTimeZoneProvider _dateTimeZoneProvider;

        public TemplatesController(IMapper mapper,
            ITemplateQuery templateQuery,
            ITemplateRepository templateRepository,
            IDateTimeZoneProvider dateTimeZoneProvider)
        {
            _mapper = mapper;
            _templateQuery = templateQuery;
            _templateRepository = templateRepository;
            _dateTimeZoneProvider = dateTimeZoneProvider;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(ViewTemplate), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ViewTemplate>> GetAsync(int id, CancellationToken cancellationToken)
        {
            var template = await _templateQuery.GetAsync(id, cancellationToken);
            if (template is null) return NotFound();
            return _mapper.Map<ViewTemplate>(template);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ViewTemplate[]), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ViewTemplate[]>> ListAllAsync(CancellationToken cancellationToken)
        {
            var templates = await _templateQuery.ListAsync(cancellationToken);
            return _mapper.Map<ViewTemplate[]>(templates);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ViewTemplate), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAsync([FromBody]CreateTemplate request, CancellationToken cancellationToken)
        {
            var billingDate = _mapper.Map<Domain.Templates.BillingDate>(request.BillingDate);
            var timeZone = _dateTimeZoneProvider[request.TimeZone];
            var template = new Template(request.Name, request.Price, request.BillingModeId, billingDate, request.BillingPeriodMonth, request.BillingPeriodOffsetMonth, timeZone);
            _templateRepository.Add(template);
            await _templateRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            var vm = _mapper.Map<ViewTemplate>(template);
            return CreatedAtAction(nameof(GetAsync), new { id = vm.Id }, vm);
        }
    }
}
