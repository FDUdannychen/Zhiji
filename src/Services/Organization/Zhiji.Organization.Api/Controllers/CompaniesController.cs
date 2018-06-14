using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zhiji.Organization.Api.Commands.Companies;
using Zhiji.Organization.Api.ViewModels;

namespace Zhiji.Organization.Api.Controllers
{
    [Route("[controller]")]
    public class CompaniesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CompaniesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<CompanyViewModel>> Get(int id)
        {
            var request = new QueryCompanyCommand { Id = id };
            var response = await _mediator.Send(request);
            var company = response.SingleOrDefault();

            if (company is null) return NotFound();

            return _mapper.Map<CompanyViewModel>(company);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IEnumerable<CompanyViewModel>> GetAll()
        {
            var request = new QueryCompanyCommand();
            var response = await _mediator.Send(request);
            return _mapper.Map<IEnumerable<CompanyViewModel>>(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CompanyViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody]CreateCompanyCommand request)
        {
            var company = await _mediator.Send(request);
            var vm = _mapper.Map<CompanyViewModel>(company);
            return CreatedAtAction(nameof(Get), new { id = vm.Id }, vm);
        }
    }
}
