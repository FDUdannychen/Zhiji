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
        [Route("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BranchViewModel>> Get(int id)
        {
            var request = new QueryBranchCommand { Id = id };
            var response = await _mediator.Send(request);
            var branch = response.SingleOrDefault();

            if (branch == null) return NotFound();

            return _mapper.Map<BranchViewModel>(branch);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IEnumerable<BranchViewModel>> GetAll()
        {
            var request = new QueryBranchCommand();
            var response = await _mediator.Send(request);
            return _mapper.Map<IEnumerable<BranchViewModel>>(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BranchViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateBranch([FromBody]CreateBranchCommand request)
        {
            var branch = await _mediator.Send(request);
            var vm = _mapper.Map<BranchViewModel>(branch);
            return CreatedAtAction(nameof(Get), new { id = vm.Id }, vm);
        }
    }
}
