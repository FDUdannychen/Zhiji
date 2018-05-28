using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Zhiji.Organization.Api.Commands.Branches;
using Zhiji.Organization.Api.ViewModels;

namespace Zhiji.Organization.Api.Controllers
{
    [Route("[controller]")]
    public class BranchesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BranchesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(BranchViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            var request = new QueryBranchCommand { Id = id };
            var response = await _mediator.Send(request);
            var branch = response.SingleOrDefault();

            if (branch == null) return NotFound();

            var vm = _mapper.Map<BranchViewModel>(branch);
            return Ok(vm);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BranchViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody]CreateBranchCommand request)
        {
            var branch = await _mediator.Send(request);
            var vm = _mapper.Map<BranchViewModel>(branch);
            return CreatedAtAction(nameof(Get), new { id = branch.Id }, vm);
        }
    }
}
